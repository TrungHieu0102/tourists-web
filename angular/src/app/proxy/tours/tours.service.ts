import type { AddUpdateTourAttributeDto, TourAttributeListFilterDto, TourAttributeValueDto } from './attributes/models';
import type { CreateUpdateTourDto, TourDto, TourInListDto, TourListFilterDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToursService {
  apiName = 'Default';
  

  addTourAttribute = (input: AddUpdateTourAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeValueDto>({
      method: 'POST',
      url: '/api/app/tours/tour-attribute',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateUpdateTourDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourDto>({
      method: 'POST',
      url: '/api/app/tours',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tours/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/tours/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourDto>({
      method: 'GET',
      url: `/api/app/tours/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourDto>>({
      method: 'GET',
      url: '/api/app/tours',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAll = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourInListDto[]>({
      method: 'GET',
      url: '/api/app/tours/all',
    },
    { apiName: this.apiName,...config });
  

  getListFilter = (input: TourListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourInListDto>>({
      method: 'GET',
      url: '/api/app/tours/filter',
      params: { categoryId: input.categoryId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListTourAttributeAll = (tourId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeValueDto[]>({
      method: 'GET',
      url: `/api/app/tours/tour-attribute-all/${tourId}`,
    },
    { apiName: this.apiName,...config });
  

  getListTourAttributes = (input: TourAttributeListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TourAttributeValueDto>>({
      method: 'GET',
      url: '/api/app/tours/tour-attributes',
      params: { tourId: input.tourId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSuggestNewCode = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/tours/suggest-new-code',
    },
    { apiName: this.apiName,...config });
  

  getThumbnailImage = (fileName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/tours/thumbnail-image',
      params: { fileName },
    },
    { apiName: this.apiName,...config });
  

  removeTourAttribute = (attributeId: string, id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/tours/${id}/tour-attribute/${attributeId}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateTourDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourDto>({
      method: 'PUT',
      url: `/api/app/tours/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateTourAttribute = (id: string, input: AddUpdateTourAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TourAttributeValueDto>({
      method: 'PUT',
      url: `/api/app/tours/${id}/tour-attribute`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
