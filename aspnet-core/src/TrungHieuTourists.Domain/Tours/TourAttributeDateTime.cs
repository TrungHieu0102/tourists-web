using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeDateTime : Entity<Guid>
    {
        public Guid AttributeId { get; set; }
        public Guid TourId { get; set; }
        public DateTime? Value { get; set; }

    }
}
