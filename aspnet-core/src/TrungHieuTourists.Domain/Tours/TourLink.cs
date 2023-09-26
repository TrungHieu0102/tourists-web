using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourLink:Entity
    {
        public Guid TourId { get; set; }
        public Guid LinkedTour { get; set; }
        public override object[] GetKeys()
        {
            return new object[] { TourId, LinkedTour };
        }

    }
}
