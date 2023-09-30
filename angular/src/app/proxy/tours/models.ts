import type { TourType } from '../trung-hieu-tourists/tours/tour-type.enum';
import type { EntityDto } from '@abp/ng.core';

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
  categoryId?: string;
  seoMetaDescription?: string;
  description?: string;
  thumbnailPicture?: string;
}

export interface TourDto {
  name?: string;
  code?: string;
  slug?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  parentId?: string;
  seoMetaDescription?: string;
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
