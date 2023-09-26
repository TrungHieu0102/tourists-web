using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeDecimail: Entity<Guid>
    {
        public Guid AttribiteId { get; set; }
        public Guid TourId { get; set; }
        public decimal? Value { get; set; }

    }
}
