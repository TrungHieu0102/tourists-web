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
using TrungHieuTourists.TourAttributes;
using TrungHieuTourists.Admin.Catalog.Tours.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace TrungHieuTourists.Admin.Catalog.Tours
{
    [Authorize] 
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
        private readonly IRepository<TourAttribute> _tourAttributeRepository;
        private readonly IRepository<TourAttributeDateTime> _tourAttributeDateTimeRepository;
        private readonly IRepository<TourAttributeInt> _tourAttributeIntRepository;
        private readonly IRepository<TourAttributeDecimal> _tourAttributeDecimalRepository;
        private readonly IRepository<TourAttributeVarchar> _tourAttributeVarcharRepository;
        private readonly IRepository<TourAttributeText> _tourAttributeTextRepository;
        public ToursAppService(IRepository<Tour, Guid> repository,
            IRepository<TourCategory> tourCategoryRepository,
            TourManager tourManager,
            IBlobContainer<TourThumbnailPictureContainer> fileContainer,
            TourCodeGenerator tourCodeGenerator,
             IRepository<TourAttribute> tourAttributeRepository,
            IRepository<TourAttributeDateTime> tourAttributeDateTimeRepository,
              IRepository<TourAttributeInt> tourAttributeIntRepository,
              IRepository<TourAttributeDecimal> tourAttributeDecimalRepository,
              IRepository<TourAttributeVarchar> tourAttributeVarcharRepository,
              IRepository<TourAttributeText> tourAttributeTextRepository)
            : base(repository)
        {
            _tourManager = tourManager;
            _tourCategoryRepository = tourCategoryRepository;
            _fileContainer = fileContainer;
            _tourCodeGenerator = tourCodeGenerator;
            _tourAttributeRepository = tourAttributeRepository;
            _tourAttributeDateTimeRepository = tourAttributeDateTimeRepository;
            _tourAttributeIntRepository = tourAttributeIntRepository;
            _tourAttributeDecimalRepository = tourAttributeDecimalRepository;
            _tourAttributeVarcharRepository = tourAttributeVarcharRepository;
            _tourAttributeTextRepository = tourAttributeTextRepository;
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
        public async Task<TourAttributeValueDto> AddTourAttributeAsync(AddUpdateTourAttributeDto input)
        {
            var tour = await Repository.GetAsync(input.TourId);
            if (tour == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourIsNotExists);

            var attribute = await _tourAttributeRepository.GetAsync(x => x.Id == input.AttributeId);
            if (attribute == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
            var newAttributeId = Guid.NewGuid();
            switch (attribute.DataType)
            {
                case AttributeType.Date:
                    if (input.DateTimeValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeDateTime = new TourAttributeDateTime(newAttributeId, input.AttributeId, input.TourId, input.DateTimeValue);
                    await _tourAttributeDateTimeRepository.InsertAsync(tourAttributeDateTime);
                    break;
                case AttributeType.Int:
                    if (input.IntValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeInt = new TourAttributeInt(newAttributeId, input.AttributeId, input.TourId, input.IntValue.Value);
                    await _tourAttributeIntRepository.InsertAsync(tourAttributeInt);
                    break;
                case AttributeType.Decimal:
                    if (input.DecimalValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeDecimal = new TourAttributeDecimal(newAttributeId, input.AttributeId, input.TourId, input.DecimalValue.Value);
                    await _tourAttributeDecimalRepository.InsertAsync(tourAttributeDecimal);
                    break;
                case AttributeType.Varchar:
                    if (input.VarcharValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeVarchar = new TourAttributeVarchar(newAttributeId, input.AttributeId, input.TourId, input.VarcharValue);
                    await _tourAttributeVarcharRepository.InsertAsync(tourAttributeVarchar);
                    break;
                case AttributeType.Text:
                    if (input.TextValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeText = new TourAttributeText(newAttributeId, input.AttributeId, input.TourId, input.TextValue);
                    await _tourAttributeTextRepository.InsertAsync(tourAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new TourAttributeValueDto()
            {
                AttributeId = input.AttributeId,
                Code = attribute.Code,
                DataType = attribute.DataType,
                DateTimeValue = input.DateTimeValue,
                DecimalValue = input.DecimalValue,
                Id = newAttributeId,
                IntValue = input.IntValue,
                Label = attribute.Label,
                TourId = input.TourId,
                TextValue = input.TextValue
            };
        }

        public async Task RemoveTourAttributeAsync(Guid attributeId, Guid id)
        {
            var attribute = await _tourAttributeRepository.GetAsync(x => x.Id == attributeId);
            if (attribute == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
            switch (attribute.DataType)
            {
                case AttributeType.Date:
                    var tourAttributeDateTime = await _tourAttributeDateTimeRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeDateTime == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    await _tourAttributeDateTimeRepository.DeleteAsync(tourAttributeDateTime);
                    break;
                case AttributeType.Int:

                    var tourAttributeInt = await _tourAttributeIntRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeInt == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    await _tourAttributeIntRepository.DeleteAsync(tourAttributeInt);
                    break;
                case AttributeType.Decimal:
                    var tourAttributeDecimal = await _tourAttributeDecimalRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeDecimal == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    await _tourAttributeDecimalRepository.DeleteAsync(tourAttributeDecimal);
                    break;
                case AttributeType.Varchar:
                    var tourAttributeVarchar = await _tourAttributeVarcharRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeVarchar == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    await _tourAttributeVarcharRepository.DeleteAsync(tourAttributeVarchar);
                    break;
                case AttributeType.Text:
                    var tourAttributeText = await _tourAttributeTextRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeText == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    await _tourAttributeTextRepository.DeleteAsync(tourAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<List<TourAttributeValueDto>> GetListTourAttributeAllAsync(Guid tourId)
        {
            var attributeQuery = await _tourAttributeRepository.GetQueryableAsync();

            var attributeDateTimeQuery = await _tourAttributeDateTimeRepository.GetQueryableAsync();
            var attributeDecimalQuery = await _tourAttributeDecimalRepository.GetQueryableAsync();
            var attributeIntQuery = await _tourAttributeIntRepository.GetQueryableAsync();
            var attributeVarcharQuery = await _tourAttributeVarcharRepository.GetQueryableAsync();
            var attributeTextQuery = await _tourAttributeTextRepository.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDateTimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimalTable
                        from adecimal in aDecimalTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join aVarchar in attributeVarcharQuery on a.Id equals aVarchar.AttributeId into aVarcharTable
                        from aVarchar in aVarcharTable.DefaultIfEmpty()
                        join aText in attributeTextQuery on a.Id equals aText.AttributeId into aTextTable
                        from aText in aTextTable.DefaultIfEmpty()
                        where (adate == null || adate.TourId == tourId)
                        && (adecimal == null || adecimal.TourId == tourId)
                         && (aint == null || aint.TourId == tourId)
                          && (aVarchar == null || aVarchar.TourId == tourId)
                           && (aText == null || aText.TourId == tourId)
                        select new TourAttributeValueDto()
                        {
                            Label = a.Label,
                            AttributeId = a.Id,
                            DataType = a.DataType,
                            Code = a.Code,
                            TourId = tourId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            TextValue = aText != null ? aText.Value : null,
                            VarcharValue = aVarchar != null ? aVarchar.Value : null,
                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            TextId = aText != null ? aText.Id : null,
                            VarcharId = aVarchar != null ? aVarchar.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null
                          || x.DecimalId != null
                          || x.IntValue != null
                          || x.TextId != null
                          || x.VarcharId != null);
            return await AsyncExecuter.ToListAsync(query);
        }

        public async Task<PagedResultDto<TourAttributeValueDto>> GetListTourAttributesAsync(TourAttributeListFilterDto input)
        {
            var attributeQuery = await _tourAttributeRepository.GetQueryableAsync();

            var attributeDateTimeQuery = await _tourAttributeDateTimeRepository.GetQueryableAsync();
            var attributeDecimalQuery = await _tourAttributeDecimalRepository.GetQueryableAsync();
            var attributeIntQuery = await _tourAttributeIntRepository.GetQueryableAsync();
            var attributeVarcharQuery = await _tourAttributeVarcharRepository.GetQueryableAsync();
            var attributeTextQuery = await _tourAttributeTextRepository.GetQueryableAsync();

            var query = from a in attributeQuery
                        join adate in attributeDateTimeQuery on a.Id equals adate.AttributeId into aDateTimeTable
                        from adate in aDateTimeTable.DefaultIfEmpty()
                        join adecimal in attributeDecimalQuery on a.Id equals adecimal.AttributeId into aDecimalTable
                        from adecimal in aDecimalTable.DefaultIfEmpty()
                        join aint in attributeIntQuery on a.Id equals aint.AttributeId into aIntTable
                        from aint in aIntTable.DefaultIfEmpty()
                        join aVarchar in attributeVarcharQuery on a.Id equals aVarchar.AttributeId into aVarcharTable
                        from aVarchar in aVarcharTable.DefaultIfEmpty()
                        join aText in attributeTextQuery on a.Id equals aText.AttributeId into aTextTable
                        from aText in aTextTable.DefaultIfEmpty()
                        where (adate == null || adate.TourId == input.TourId)
                        && (adecimal == null || adecimal.TourId == input.TourId)
                         && (aint == null || aint.TourId == input.TourId)
                          && (aVarchar == null || aVarchar.TourId == input.TourId)
                           && (aText == null || aText.TourId == input.TourId)
                        select new TourAttributeValueDto()
                        {
                            Label = a.Label,
                            AttributeId = a.Id,
                            DataType = a.DataType,
                            Code = a.Code,
                            TourId = input.TourId,
                            DateTimeValue = adate != null ? adate.Value : null,
                            DecimalValue = adecimal != null ? adecimal.Value : null,
                            IntValue = aint != null ? aint.Value : null,
                            TextValue = aText != null ? aText.Value : null,
                            VarcharValue = aVarchar != null ? aVarchar.Value : null,
                            DateTimeId = adate != null ? adate.Id : null,
                            DecimalId = adecimal != null ? adecimal.Id : null,
                            IntId = aint != null ? aint.Id : null,
                            TextId = aText != null ? aText.Id : null,
                            VarcharId = aVarchar != null ? aVarchar.Id : null,
                        };
            query = query.Where(x => x.DateTimeId != null
           || x.DecimalId != null
           || x.IntValue != null
           || x.TextId != null
           || x.VarcharId != null);
            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(
                query.OrderByDescending(x => x.Label)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                );
            return new PagedResultDto<TourAttributeValueDto>(totalCount, data);
        }

        public async Task<TourAttributeValueDto> UpdateTourAttributeAsync(Guid id, AddUpdateTourAttributeDto input)
        {
            var tour = await Repository.GetAsync(input.TourId);
            if (tour == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourIsNotExists);

            var attribute = await _tourAttributeRepository.GetAsync(x => x.Id == input.AttributeId);
            if (attribute == null)
                throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);

            switch (attribute.DataType)
            {
                case AttributeType.Date:
                    if (input.DateTimeValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeDateTime = await _tourAttributeDateTimeRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeDateTime == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    tourAttributeDateTime.Value = input.DateTimeValue.Value;
                    await _tourAttributeDateTimeRepository.UpdateAsync(tourAttributeDateTime);
                    break;
                case AttributeType.Int:
                    if (input.IntValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeInt = await _tourAttributeIntRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeInt == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    tourAttributeInt.Value = input.IntValue.Value;
                    await _tourAttributeIntRepository.UpdateAsync(tourAttributeInt);
                    break;
                case AttributeType.Decimal:
                    if (input.DecimalValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeDecimal = await _tourAttributeDecimalRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeDecimal == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    tourAttributeDecimal.Value = input.DecimalValue.Value;
                    await _tourAttributeDecimalRepository.UpdateAsync(tourAttributeDecimal);
                    break;
                case AttributeType.Varchar:
                    if (input.VarcharValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeVarchar = await _tourAttributeVarcharRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeVarchar == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    tourAttributeVarchar.Value = input.VarcharValue;
                    await _tourAttributeVarcharRepository.UpdateAsync(tourAttributeVarchar);
                    break;
                case AttributeType.Text:
                    if (input.TextValue == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeValueIsNotValid);
                    }
                    var tourAttributeText = await _tourAttributeTextRepository.GetAsync(x => x.Id == id);
                    if (tourAttributeText == null)
                    {
                        throw new BusinessException(TrungHieuTouristsDomainErrorCodes.TourAttributeIdIsNotExists);
                    }
                    tourAttributeText.Value = input.TextValue;
                    await _tourAttributeTextRepository.UpdateAsync(tourAttributeText);
                    break;
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return new TourAttributeValueDto()
            {
                AttributeId = input.AttributeId,
                Code = attribute.Code,
                DataType = attribute.DataType,
                DateTimeValue = input.DateTimeValue,
                DecimalValue = input.DecimalValue,
                Id = id,
                IntValue = input.IntValue,
                Label = attribute.Label,
                TourId = input.TourId,
                TextValue = input.TextValue
            };
        }
    }
}

