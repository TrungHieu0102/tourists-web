using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Tours.Attributes
{
    public class TourAttributeListFilterDto : BaseListFilterDto
    {
        public Guid TourId { get; set; }
    }
}
