using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.Countries
{
    public class CreateUpdateCountryDtoValidator : AbstractValidator<CreateUpdateCountryDto>
    {
        public CreateUpdateCountryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CoverPicture).MaximumLength(250);
        }
    }
}
