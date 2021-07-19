using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MyPersonalBlog.DTO.DTOs.CategoryBlogDtos;

namespace MyPersonalBlog.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogAddValidator:AbstractValidator<CategoryBlogAddDto>
    {
        public CategoryBlogAddValidator()
        {
            RuleFor(I => I.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("CaegoryId boş geçilemez");
            RuleFor(I => I.BlogId).InclusiveBetween(0, int.MaxValue).WithMessage("BlogId boş geçilemez");
        }
    }
}
