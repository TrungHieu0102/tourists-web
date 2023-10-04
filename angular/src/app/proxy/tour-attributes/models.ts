import type { AttributeType } from '../trung-hieu-tourists/tour-attributes/attribute-type.enum';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateTourAttributeDto {
  code?: string;
  dataType: AttributeType;
  label?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  note?: string;
}

export interface TourAttributeDto {
  code?: string;
  dataType: AttributeType;
  label?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  note?: string;
  id?: string;
}

export interface TourAttributeInListDto extends EntityDto<string> {
  code?: string;
  dataType: AttributeType;
  label?: string;
  sortOrder: number;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  id?: string;
}
