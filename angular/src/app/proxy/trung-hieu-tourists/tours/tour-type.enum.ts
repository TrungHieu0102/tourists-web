import { mapEnumToOptions } from '@abp/ng.core';

export enum TourType {
  Single = 1,
  Grouped = 2,
  Configurable = 3,
  Bundle = 4,
  Virtual = 5,
}

export const tourTypeOptions = mapEnumToOptions(TourType);
