using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeDecimal: Entity<Guid>
    {
        public TourAttributeDecimal(Guid id, Guid attributeId, Guid tourId, decimal value)
        {
            Id = id;
            AttributeId = attributeId;
            TourId = tourId;
            Value = value;
        }
        public Guid AttributeId { get; set; }
        public Guid TourId { get; set; }
        public decimal Value { get; set; }

    }
}
