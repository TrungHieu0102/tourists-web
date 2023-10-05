import type { CreateUpdateTourCategoryDto, TourCategoryDto, TourCategoryInListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';

@Injectable({
  providedIn: 'root',
})
export class TourCategoriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTourCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourCategoryDto>({
      method: 'POST',
      url: '/api/app/tour-categories',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tour-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tour-categories/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourCategoryDto>({
      method: 'GET',
      url: `/api/app/tour-categories/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourCategoryDto>>({
      method: 'GET',
      url: '/api/app/tour-categories',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAll = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourCategoryInListDto[]>({
      method: 'GET',
      url: '/api/app/tour-categories/all',
    },
    { apiName: this.apiName,...config });
  

  getListFilter = (input: BaseListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourCategoryInListDto>>({
      method: 'GET',
      url: '/api/app/tour-categories/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateTourCategoryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourCategoryDto>({
      method: 'PUT',
      url: `/api/app/tour-categories/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
