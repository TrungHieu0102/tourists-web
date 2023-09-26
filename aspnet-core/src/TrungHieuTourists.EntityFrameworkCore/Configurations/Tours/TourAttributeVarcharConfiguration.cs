using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeVarcharConfiguration : IEntityTypeConfiguration<TourAttributeVarchar>
    {
        public void Configure(EntityTypeBuilder<TourAttributeVarchar> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributeVarchars");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value).HasMaxLength(500);
        }
    }
}
