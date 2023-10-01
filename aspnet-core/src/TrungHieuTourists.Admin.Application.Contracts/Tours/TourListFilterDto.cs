using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Tours
{
    public class TourListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
