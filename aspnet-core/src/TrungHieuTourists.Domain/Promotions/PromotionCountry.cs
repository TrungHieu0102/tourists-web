using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Promotions
{
    public class PromotionCountry : Entity<Guid>
    {
        public Guid CountryId { get; set; }
        public Guid PromotionId { get; set; }
    }
}
