using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TrungHieuTourists.Tours
{
    public class TourAttributeTextConfiguration : IEntityTypeConfiguration<TourAttributeText>
    {
        public void Configure(EntityTypeBuilder<TourAttributeText> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributeTexts");
            builder.HasKey(x => x.Id);
        }
    }
}
