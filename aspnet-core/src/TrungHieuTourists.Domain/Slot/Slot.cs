using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TrungHieuTourists.Slot
{
    public class Slot :AuditedAggregateRoot<Guid>
    {
        public Guid TourId { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
    }
}
