﻿using System;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeText: Entity<Guid>
    {
        public Guid AttributeId { get; set; }
        public Guid TourId { get; set; }
        public string Value { get; set; }

    }
}