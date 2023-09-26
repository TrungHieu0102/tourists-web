﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeInt : Entity<Guid>
    {
        public Guid AttributeId { get; set; }
        public Guid TourId { get; set; }
        public int? Value { get; set; }

    }
}
