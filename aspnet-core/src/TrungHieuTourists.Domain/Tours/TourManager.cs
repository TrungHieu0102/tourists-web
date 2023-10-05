using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungHieuTourists.TourCategoris;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace TrungHieuTourists.Tours
{
    public class TourManager : DomainService
    {
        private readonly IRepository<Tour, Guid> _tourRepository;
        private readonly IRepository<TourCategory, Guid> _tourCategoryRepository;
        public TourManager(IRepository<Tour, Guid> tourRepository,
             IRepository<TourCategory, Guid> tourCategoryRepository)
        {
            _tourCategoryRepository = tourCategoryRepository;
            _tourRepository = tourRepository;
        }

        public async Task<Tour> CreateAsync(Guid manufacturerId,
            string name, string code, string slug,
            TourType tourType, string sKU,
            int sortOrder, bool visibility,
            bool isActive, Guid categoryId,
            string seoMetaDescription, string description,
             double sellPrice)
        {
            if (await _tourRepository.AnyAsync(x => x.Name == name))
                throw new UserFriendlyException("Tên tour đã tồn tại", TrungHieuTouristsDomainErrorCodes.TourNameAlreadyExists);
            if (await _tourRepository.AnyAsync(x => x.Code == code))
                throw new UserFriendlyException("Mã tour đã tồn tại", TrungHieuTouristsDomainErrorCodes.TourCodeAlreadyExists);
            if (await _tourRepository.AnyAsync(x => x.SKU == sKU))
                throw new UserFriendlyException("Mã SKU tour đã tồn tại", TrungHieuTouristsDomainErrorCodes.TourSKUAlreadyExists);

            var category = await _tourCategoryRepository.GetAsync(categoryId);

            return new Tour(Guid.NewGuid(), manufacturerId, name, code, slug, tourType, sKU, sortOrder,
                visibility, isActive, categoryId, seoMetaDescription, description, null, sellPrice, category?.Name, category?.Slug);
        }
    }
}
