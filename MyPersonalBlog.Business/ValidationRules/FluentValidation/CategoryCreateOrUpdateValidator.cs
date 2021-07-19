using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MyPersonalBlog.DTO.DTOs.CategoryDtos;

namespace MyPersonalBlog.Business.ValidationRules.FluentValidation
{
    public class CategoryCreateOrUpdateValidator:AbstractValidator<CategoryCreateOrUpdateDto>
    {
        public CategoryCreateOrUpdateValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Ad Alanı Gereklidir").MaximumLength(100).WithMessage("Ad alanı 100 karakterden az olmalıdır");
            RuleFor(I => I.Description).NotNull().WithMessage("İçerik Alanı Gereklidir");
        }
    }
}
