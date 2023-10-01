import type { TourType } from '../trung-hieu-tourists/tours/tour-type.enum';
import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface CreateUpdateTourDto {
  countryId?: string;
  name?: string;
  code?: string;
  slug?: string;
  tourType: TourType;
  sku?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  sellPrice: number;
  categoryId?: string;
  seoMetaDescription?: string;
  description?: string;
  thumbnailPicture?: string;
}

export interface TourDto {
  countryId: null;
  name?: string;
  code?: string;
  slug?: string;
  tourType: TourType;
  sku?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  categoryId?: string;
  seoMetaDescription?: string;
  description?: string;
  thumbnailPicture?: string;
  sellPrice: number;
  id?: string;
}

export interface TourInListDto extends EntityDto<string> {
  countryId?: string;
  name?: string;
  code?: string;
  slug?: string;
  tourType: TourType;
  sku?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  categoryId?: string;
  thumbnailPicture?: string;
}

export interface TourListFilterDto extends BaseListFilterDto {
  categoryId?: string;
}
