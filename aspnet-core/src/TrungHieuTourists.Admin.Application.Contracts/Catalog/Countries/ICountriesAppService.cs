using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists.Admin.Catalog.Countries
{
    public interface ICountriesAppService : ICrudAppService
        <CountryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateCountryDto,
        CreateUpdateCountryDto>
    {
        Task<PagedResultDto<CountryInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<CountryInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    }
}
