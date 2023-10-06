using Microsoft.AspNetCore.Authorization;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrungHieuTourists.Admin.Permissions;
using TrungHieuTourists.TourAttributes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TrungHieuTourists.Admin.Catalog.TourAttributes
{
   
    public class TourAttributesAppService : CrudAppService<
        TourAttribute,
        TourAttributeDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourAttributeDto,
        CreateUpdateTourAttributeDto>, ITourAttributesAppService
    {
        public TourAttributesAppService(IRepository<TourAttribute, Guid> repository)
            : base(repository)
        {
            GetPolicyName =TrungHieuTouristsPermissions.Attribute.Default;
            GetListPolicyName =TrungHieuTouristsPermissions.Attribute.Default;
            CreatePolicyName =TrungHieuTouristsPermissions.Attribute.Create;
            UpdatePolicyName =TrungHieuTouristsPermissions.Attribute.Update;
            DeletePolicyName =TrungHieuTouristsPermissions.Attribute.Delete;
        }
       
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
      
        public async Task<List<TourAttributeInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<TourAttribute>, List<TourAttributeInListDto>>(data);
        }
      
        public async Task<PagedResultDto<TourAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Label.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<TourAttributeInListDto>(totalCount, ObjectMapper.Map<List<TourAttribute>, List<TourAttributeInListDto>>(data));
        }
    }
}