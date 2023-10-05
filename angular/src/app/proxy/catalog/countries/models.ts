import type { EntityDto } from '@abp/ng.core';

export interface CountryDto {
  name?: string;
  code?: string;
  slug?: string;
  coverPicture?: string;
  visibility: boolean;
  isActive: boolean;
  continent?: string;
  id?: string;
}

export interface CountryInListDto extends EntityDto<string> {
  name?: string;
  code?: string;
  slug?: string;
  coverPicture?: string;
  visibility: boolean;
  isActive: boolean;
  continent?: string;
}

export interface CreateUpdateCountryDto {
  name?: string;
  code?: string;
  slug?: string;
  coverPicture?: string;
  visibility: boolean;
  isActive: boolean;
  continent?: string;
}
