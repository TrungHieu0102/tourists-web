using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungHieuTourists.Tours;

namespace TrungHieuTourists.Configurations.Tours
{
    public class TourTagConfiguration : IEntityTypeConfiguration<TourTag>
    {
        public void Configure(EntityTypeBuilder<TourTag> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourTags");
            builder.HasKey(x => new { x.TourId, x.TagId });
        }
    }
}
