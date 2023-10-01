using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TrungHieuTourists.TourAttributes
{
    public class TourAttributeConfiguration : IEntityTypeConfiguration<TourAttribute>
    {
        public void Configure(EntityTypeBuilder<TourAttribute> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourAttributes");
            builder.HasKey(t => t.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Label)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
