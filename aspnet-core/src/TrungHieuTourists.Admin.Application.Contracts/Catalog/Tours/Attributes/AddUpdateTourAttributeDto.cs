using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.Tours.Attributes
{
    public class AddUpdateTourAttributeDto
    {
        public Guid TourId { get; set; }
        public Guid AttributeId { get; set; }

        public DateTime? DateTimeValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public int? IntValue { get; set; }
        public string VarcharValue { get; set; }

        public string TextValue { get; set; }
    }
}
