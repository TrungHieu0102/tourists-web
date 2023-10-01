using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TrungHieuTourists.Tours
{
    public class TourReview :CreationAuditedEntity<Guid>
    {
        public Guid TourId { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Content { get; set; }
        public Guid OrderId  { get; set; }

    }
}
