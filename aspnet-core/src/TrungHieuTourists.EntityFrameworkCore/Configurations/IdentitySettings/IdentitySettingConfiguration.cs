using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrungHieuTourists.IdentitySettings
{
    public class IdentitySettingConfiguration : IEntityTypeConfiguration<IdentitySetting>
    {
        public void Configure(EntityTypeBuilder<IdentitySetting> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "IdentitySettings",
                 TrungHieuTouristsConsts.DbSchema); 
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

        }
    }
}
