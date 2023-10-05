using System;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeText: Entity<Guid>
    {
        public TourAttributeText(Guid id, Guid attributeId, Guid tourId, string value)
        {
            Id = id;
            AttributeId = attributeId;
            TourId = tourId;
            Value = value;
        }
        public Guid AttributeId { get; set; }
        public Guid TourId { get; set; }
        public string Value { get; set; }

    }
}
