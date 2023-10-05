import type { BaseListFilterDto } from '../../../models';
import type { AttributeType } from '../../../trung-hieu-tourists/tour-attributes/attribute-type.enum';

export interface AddUpdateTourAttributeDto {
  tourId?: string;
  attributeId?: string;
  dateTimeValue?: string;
  decimalValue?: number;
  intValue?: number;
  varcharValue?: string;
  textValue?: string;
}

export interface TourAttributeListFilterDto extends BaseListFilterDto {
  tourId?: string;
}

export interface TourAttributeValueDto {
  id?: string;
  tourId?: string;
  attributeId?: string;
  code?: string;
  dataType: AttributeType;
  label?: string;
  dateTimeValue?: string;
  decimalValue?: number;
  intValue?: number;
  textValue?: string;
  varcharValue?: string;
  dateTimeId?: string;
  decimalId?: string;
  intId?: string;
  textId?: string;
  varcharId?: string;
}
