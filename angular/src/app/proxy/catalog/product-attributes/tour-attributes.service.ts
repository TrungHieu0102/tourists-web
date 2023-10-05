import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../../models';
import type { CreateUpdateTourAttributeDto, TourAttributeDto, TourAttributeInListDto } from '../tour-attributes/models';

@Injectable({
  providedIn: 'root',
})
export class TourAttributesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTourAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeDto>({
      method: 'POST',
      url: '/api/app/tour-attributes',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tour-attributes/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tour-attributes/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeDto>({
      method: 'GET',
      url: `/api/app/tour-attributes/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourAttributeDto>>({
      method: 'GET',
      url: '/api/app/tour-attributes',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAll = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeInListDto[]>({
      method: 'GET',
      url: '/api/app/tour-attributes/all',
    },
    { apiName: this.apiName,...config });
  

  getListFilter = (input: BaseListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourAttributeInListDto>>({
      method: 'GET',
      url: '/api/app/tour-attributes/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateTourAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeDto>({
      method: 'PUT',
      url: `/api/app/tour-attributes/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
