using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeDateTimeConfiguration : IEntityTypeConfiguration<TourAttributeDateTime>
    {
        public void Configure(EntityTypeBuilder<TourAttributeDateTime> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributeDateTimes");
            builder.HasKey(x => x.Id);
        }
    }
}
