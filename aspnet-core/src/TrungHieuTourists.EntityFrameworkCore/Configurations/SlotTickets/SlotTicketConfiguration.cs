using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungHieuTourists.SlotTickets
{
    public class SlotTicketConfiguration : IEntityTypeConfiguration<SlotTicket>
    {
        public void Configure(EntityTypeBuilder<SlotTicket> builder)
        {
            builder.ToTable(TrungHieuTouristsConsts.DbTablePrefix + "SlotTickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
