using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeDecimalConfiguration : IEntityTypeConfiguration<TourAttributeDecimal>
    {
        public void Configure(EntityTypeBuilder<TourAttributeDecimal> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributeDecimals");
            builder.HasKey(x => x.Id);
        }
    }
}
