using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MyPersonalBlog.DTO.DTOs.BlogDtos;

namespace MyPersonalBlog.Business.ValidationRules.FluentValidation
{
    public class BlogCreateOrUpdateValidator:AbstractValidator<BlogCreateOrUpdateDto>
    {
        public BlogCreateOrUpdateValidator()
        {
            RuleFor(I => I.Title).NotNull().WithMessage("Başlık Alanı Gereklidir").MaximumLength(100).WithMessage("Başlık 100 karakterden az olmalıdır");
            RuleFor(I => I.ShortDescription).NotNull().WithMessage("Kısa İçerik Alanı Gereklidir").MaximumLength(300).WithMessage("Kısa içerik 300 karakterden az olmalıdı");
            RuleFor(I => I.Description).NotNull().WithMessage("İçerik Alanı Gereklidir");
        }
    }
}
