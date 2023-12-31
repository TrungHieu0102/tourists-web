﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrungHieuTourists.Admin.Catalog.TourCategories
{
    public class CreateUpdateTourCategoryDtoValidator : AbstractValidator
        <CreateUpdateTourCategoryDto>
    {
        public CreateUpdateTourCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Slug).NotEmpty().MaximumLength(50);
            RuleFor(x => x.CoverPicture).MaximumLength(250);
            RuleFor(x => x.SeoMetaDescription).MaximumLength(250);
        }
    }
}
