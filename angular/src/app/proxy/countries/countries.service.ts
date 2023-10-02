import type { CountryDto, CountryInListDto, CreateUpdateCountryDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class CountriesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCountryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>({
      method: 'POST',
      url: '/api/app/countries',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/countries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/countries/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>({
      method: 'GET',
      url: `/api/app/countries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CountryDto>>({
      method: 'GET',
      url: '/api/app/countries',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAll = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryInListDto[]>({
      method: 'GET',
      url: '/api/app/countries/all',
    },
    { apiName: this.apiName,...config });
  

  getListFilter = (input: BaseListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CountryInListDto>>({
      method: 'GET',
      url: '/api/app/countries/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateCountryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>({
      method: 'PUT',
      url: `/api/app/countries/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
