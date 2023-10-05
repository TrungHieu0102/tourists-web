using System;
using TrungHieuTourists.TourAttributes;
using Volo.Abp.Application.Dtos;

namespace TrungHieuTourists.Admin.Catalog.TourAttributes
{
    public class TourAttributeInListDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public AttributeType DataType { get; set; }
        public string Label { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public bool IsRequired { get; set; }
        public bool IsUnique { get; set; }
        public Guid Id { get; set; }
    }
}
