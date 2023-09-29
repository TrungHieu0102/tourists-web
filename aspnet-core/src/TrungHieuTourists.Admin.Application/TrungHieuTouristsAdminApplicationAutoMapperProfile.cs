using AutoMapper;
using TrungHieuTourists.Admin.TourCategories;
using TrungHieuTourists.TourCategoris;

namespace TrungHieuTourists.Admin;

public class TrungHieuTouristsAdminApplicationAutoMapperProfile : Profile
{
    public TrungHieuTouristsAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<TourCategory, TourCategoryDto>();
        CreateMap<TourCategory, TourCategoryInListDto>();
        CreateMap<CreateUpdateTourCategoryDto, TourCategory>();
    }
}
