using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.Tours
{
    public class TourListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
