using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;

namespace MyPersonalBlog.Business.ValidationRules.FluentValidation
{
    public class AppUserUpdateValidator:AbstractValidator<AppUserUpdateDto>
    {
        public AppUserUpdateValidator()
        {
            RuleFor(I => I.Name).NotNull().WithMessage("Ad Alanı Boş Geçilemez").MaximumLength(100).WithMessage("100 Karakterden Az Bir İsim Giriniz");
            RuleFor(I => I.SurName).NotNull().WithMessage("Soyad Alanı Boş Geçilemez").MaximumLength(100).WithMessage("100 Karakterden az bir soyisim giriniz");
            RuleFor(I => I.PhoneNumber).NotNull().WithMessage("Telefon Numarası Alanı Boş Geçilemez").MaximumLength(11).WithMessage("11 Numara fazla Uzun");
            RuleFor(I => I.Email).NotNull().WithMessage("Email alnı boş geçilemez").EmailAddress()
                .WithMessage("Geçersiz email adresi");
            RuleFor(I => I.GitHubAddress).NotNull().WithMessage("GitHub Adres Alanı Boş Geçilemez").MaximumLength(200).WithMessage("200 Karakterden az bir adres giriniz");
            RuleFor(I => I.FacebookAddress).NotNull().WithMessage("Facebook Adres Alanı Boş Geçilemez").MaximumLength(200).WithMessage("200 Karakterden az bir adres giriniz");
            RuleFor(I => I.YouTubeAddress).NotNull().WithMessage("YouTube Adres Alanı Boş Geçilemez").MaximumLength(200).WithMessage("200 Karakterden az bir adres giriniz");
            RuleFor(I => I.LinkedInAddress).NotNull().WithMessage("LinkedIn Adres Alanı Boş Geçilemez").MaximumLength(200).WithMessage("200 Karakterden az bir adres giriniz");
            RuleFor(I => I.Autobiography).NotNull().WithMessage("Özgeçmiş Alanı Boş Geçilemez");
        }
    }
}
