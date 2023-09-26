using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungHieuTourists.Promotions
{
    public class PromotionCountryConfiguration : IEntityTypeConfiguration<PromotionCountry>
    {
        public void Configure(EntityTypeBuilder<PromotionCountry> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + " PromotionCountries");
            builder.HasKey(x => x.Id);
        }
    }
}
