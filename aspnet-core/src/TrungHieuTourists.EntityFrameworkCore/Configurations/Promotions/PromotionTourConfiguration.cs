using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungHieuTourists.Promotions
{
    public class PromotionTourConfiguration : IEntityTypeConfiguration<PromotionTour>
    {
        public void Configure(EntityTypeBuilder<PromotionTour> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "PromotionTour");
            builder.HasKey(x => x.Id);
        }
    }
}
