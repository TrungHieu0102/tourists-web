﻿using System;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeVarchar:Entity<Guid>
    {
        public TourAttributeVarchar(Guid id, Guid attributeId, Guid tourId, string value)
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
