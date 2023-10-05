using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.Tours.Attributes
{
    public class AddUpdateTourAttributeDtoValidator : AbstractValidator<AddUpdateTourAttributeDto>
    {

        public AddUpdateTourAttributeDtoValidator()
        {
            RuleFor(x => x.TourId).NotEmpty();
            RuleFor(x => x.AttributeId).NotEmpty();
        }

    }
}
