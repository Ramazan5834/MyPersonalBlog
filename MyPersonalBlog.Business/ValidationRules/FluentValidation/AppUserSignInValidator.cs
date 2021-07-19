using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;

namespace MyPersonalBlog.Business.ValidationRules.FluentValidation
{
    public class AppUserSignInValidator:AbstractValidator<AppUserSignInDto>
    {
        public AppUserSignInValidator()
        {
            RuleFor(I => I.UserName).NotNull().WithMessage("Kullanıcı Adı Boş Geçilemez");
            RuleFor(I => I.Password).NotNull().WithMessage("Şifre alanı boş geçilemez");
        }
    }
}
