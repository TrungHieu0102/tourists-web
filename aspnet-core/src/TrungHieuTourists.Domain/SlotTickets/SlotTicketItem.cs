using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.SlotTickets
{
    public class SlotTicketItem :Entity<Guid>
    {
        public Guid TicketId { get; set; }
        public Guid TourId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
