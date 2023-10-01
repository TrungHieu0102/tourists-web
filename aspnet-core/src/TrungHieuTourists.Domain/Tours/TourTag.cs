using System;
using Volo.Abp.Domain.Entities;

namespace TrungHieuTourists.Tours
{
    public class TourTag :Entity
    {
        public Guid TourId { get; set; }
        public string TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { TourId, TagId };
        }
    }
}
