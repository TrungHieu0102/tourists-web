using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists.Admin.Catalog.TourCategories
{
    public interface ITourCategoriesAppService : ICrudAppService
        <TourCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourCategoryDto,
        CreateUpdateTourCategoryDto>
    {
        Task<PagedResultDto<TourCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<TourCategoryInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    }
}
