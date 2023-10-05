using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.TourAttributes
{
    public class CreateUpdateTourAttributeDtoValidator : AbstractValidator<CreateUpdateTourAttributeDto>
    {
        public CreateUpdateTourAttributeDtoValidator()
        {
            RuleFor(x => x.Label).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DataType).NotNull();
        }
    }
}
