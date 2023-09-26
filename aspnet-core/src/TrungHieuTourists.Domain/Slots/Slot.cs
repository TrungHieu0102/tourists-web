using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TrungHieuTourists.Slots
{
    public class Slot :AuditedAggregateRoot<Guid>
    {
        public Guid TourId { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
    }
}
