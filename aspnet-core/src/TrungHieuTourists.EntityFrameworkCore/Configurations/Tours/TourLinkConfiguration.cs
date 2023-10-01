using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TrungHieuTourists.Tours
{
    public class TourLinkConfiguration : IEntityTypeConfiguration<TourLink>
    {
        public void Configure(EntityTypeBuilder<TourLink> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourLinks");
            builder.HasKey(x => new { x.TourId, x.LinkedTourId });
        }
    }
}
