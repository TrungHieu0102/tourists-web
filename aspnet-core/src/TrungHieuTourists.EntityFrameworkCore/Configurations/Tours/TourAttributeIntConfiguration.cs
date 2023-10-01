using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeIntConfiguration : IEntityTypeConfiguration<TourAttributeInt>
    {
        public void Configure(EntityTypeBuilder<TourAttributeInt> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributeInts");
            builder.HasKey(x => x.Id);
        }
    }
}
