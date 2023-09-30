using AutoMapper;
using TrungHieuTourists.Admin.TourCategories;
using TrungHieuTourists.Admin.Tours;
using TrungHieuTourists.TourCategoris;
using TrungHieuTourists.Tours;

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

        //Tour
        CreateMap<Tour, TourDto>();
        CreateMap<Tour, TourInListDto>();
        CreateMap<CreateUpdateTourDto, Tour>();

    }
}
