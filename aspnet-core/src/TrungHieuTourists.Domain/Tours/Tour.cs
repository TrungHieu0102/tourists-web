using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TrungHieuTourists.Tours
{
    public class Tour : AuditedAggregateRoot<Guid>
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public TourType TourType { get; set; }
        public string SKU { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string SeoMetaDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
        public double SellPrice { get; set; }

    }
}
