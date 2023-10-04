using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists.Admin.TourAttributes
{
    public interface ITourAttributesAppService : ICrudAppService
        <TourAttributeDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourAttributeDto,
        CreateUpdateTourAttributeDto>
    {
        Task<PagedResultDto<TourAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<TourAttributeInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    }
}
