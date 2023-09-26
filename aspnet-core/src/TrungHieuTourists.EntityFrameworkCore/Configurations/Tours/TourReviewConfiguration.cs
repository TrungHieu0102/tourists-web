using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TrungHieuTourists.Tours
{
    public class TourReviewConfiguration : IEntityTypeConfiguration<TourReview>
    {
        public void Configure(EntityTypeBuilder<TourReview> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "TourReviews");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title)
               .HasMaxLength(250)
               .IsRequired();

        }
    }
}
