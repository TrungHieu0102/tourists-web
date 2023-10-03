using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrungHieuTourists.TourCategoris;
using TrungHieuTourists.Tours;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.BlobStoring;

namespace TrungHieuTourists.Admin.Tours
{

    public class ToursAppService : CrudAppService
        <Tour,
        TourDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourDto,
        CreateUpdateTourDto>, IToursAppService
    {
        private readonly TourManager _tourManager;
        private readonly TourCodeGenerator _tourCodeGenerator;
        private readonly IRepository<TourCategory> _tourCategoryRepository;
        private readonly IBlobContainer<TourThumbnailPictureContainer> _fileContainer;
        public ToursAppService(IRepository<Tour, Guid> repository,
            IRepository<TourCategory> tourCategoryRepository,
            TourManager tourManager,
            IBlobContainer<TourThumbnailPictureContainer> fileContainer,
            TourCodeGenerator tourCodeGenerator) : base(repository)
        {
            _tourManager = tourManager;
            _tourCategoryRepository = tourCategoryRepository;
            _fileContainer = fileContainer;
            _tourCodeGenerator = tourCodeGenerator;
        }
        public override async Task<TourDto> CreateAsync(CreateUpdateTourDto input)
        {
            var tour = await _tourManager.CreateAsync(
                input.CountryId,
                input.Name,
                input.Code,
                input.Slug,
                input.TourType,
                input.SKU,
                input.SortOrder,
                input.Visibility,
                input.IsActive,
                input.CategoryId,
                input.SeoMetaDescription,
                input.Description,
                input.SellPrice);
            if (input.ThumbnailPictureContent != null && input.ThumbnailPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.ThumbnailPictureName, input.ThumbnailPictureContent);
                tour.ThumbnailPicture = input.ThumbnailPictureName;
            }
            var result = await Repository.InsertAsync(tour);

            return ObjectMapper.Map<Tour, TourDto>(result);
        }

        public override async Task<TourDto> UpdateAsync(Guid id, CreateUpdateTourDto input)
        {
            var tour = await Repository.GetAsync(id);
            if (tour == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourIsNotExists);
            tour.CountryId = input.CountryId;
            tour.Name = input.Name;
            tour.Code = input.Code;
            tour.Slug = input.Slug;
            tour.TourType = input.TourType;
            tour.SKU = input.SKU;
            tour.SortOrder = input.SortOrder;
            tour.Visibility = input.Visibility;
            tour.IsActive = input.IsActive;

            if (tour.CategoryId != input.CategoryId)
            {
                tour.CategoryId = input.CategoryId;
                var category = await _tourCategoryRepository.GetAsync(x => x.Id == input.CategoryId);
                tour.CategoryName = category.Name;
                tour.CategorySlug = category.Slug;
            }
            tour.SeoMetaDescription = input.SeoMetaDescription;
            tour.Description = input.Description;
            if (input.ThumbnailPictureContent != null && input.ThumbnailPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.ThumbnailPictureName, input.ThumbnailPictureContent);
                tour.ThumbnailPicture = input.ThumbnailPictureName;

            }
            tour.SellPrice = input.SellPrice;
            await Repository.UpdateAsync(tour);

            return ObjectMapper.Map<Tour, TourDto>(tour);
        }

        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<TourInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Tour>, List<TourInListDto>>(data);
        }

        public async Task<PagedResultDto<TourInListDto>> GetListFilterAsync(TourListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));
            query = query.WhereIf(input.CategoryId.HasValue, x => x.CategoryId == input.CategoryId);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(
                query.OrderByDescending(x => x.CreationTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                );
            return new PagedResultDto<TourInListDto>(totalCount, ObjectMapper.Map<List<Tour>, List<TourInListDto>>(data));
        }
        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }
        public async Task<string> GetThumbnailImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

        public async Task<string> GetSuggestNewCodeAsync()
        {
            return await _tourCodeGenerator.GenerateAsync();
        }
    }
}

