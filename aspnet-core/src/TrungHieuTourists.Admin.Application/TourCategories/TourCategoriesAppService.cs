﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrungHieuTourists.TourCategoris;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TrungHieuTourists.Admin.TourCategories
{
    public class TourCategoriesAppService : CrudAppService<
        TourCategory,
        TourCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateTourCategoryDto,
        CreateUpdateTourCategoryDto>,
        ITourCategoriesAppService
    {
        private readonly IRepository<TourCategory, Guid> _repository;
        public TourCategoriesAppService(IRepository<TourCategory, Guid> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public async Task<PagedResultDto<TourCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<TourCategoryInListDto>(totalCount, ObjectMapper.Map<List<TourCategory>, List<TourCategoryInListDto>>(data));
        }
    }
}