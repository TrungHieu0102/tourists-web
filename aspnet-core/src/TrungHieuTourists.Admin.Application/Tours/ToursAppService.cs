using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungHieuTourists.Tours;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace TrungHieuTourists.Admin.Tours
{
    public class ToursAppService : CrudAppService
        < Tour,
        TourDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourDto,
        CreateUpdateTourDto>, IToursAppService
    {
        public ToursAppService(IRepository<Tour, Guid> repository)
           : base(repository)
        {
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

        public async Task<PagedResultDto<TourInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<TourInListDto>(totalCount, ObjectMapper.Map<List<Tour>, List<TourInListDto>>(data));
        }
    }
}

