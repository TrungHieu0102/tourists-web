using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrungHieuTourists.SlotTickets;

namespace TrungHieuTourists.SlotTicketItems
{
    public class SlotTicketItemConfiguration : IEntityTypeConfiguration<SlotTicketItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SlotTicketItem> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "SlotTicketItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SKU)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.BatchNumber)
               .HasMaxLength(50)
               .IsUnicode(false);

        }
    }
}
