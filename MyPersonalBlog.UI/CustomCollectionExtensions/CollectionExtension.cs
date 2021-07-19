using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyPersonalBlog.Business.ValidationRules.FluentValidation;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DTO.DTOs.AppUserDtos;
using MyPersonalBlog.DTO.DTOs.BlogDtos;
using MyPersonalBlog.DTO.DTOs.CategoryBlogDtos;
using MyPersonalBlog.DTO.DTOs.CategoryDtos;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.UI.CustomCollectionExtensions
{
    public static class CollectionExtension
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = true;
                })
                .AddEntityFrameworkStores<MyPersonalBlogContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "MyPersonalBlogCookie";
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.HttpOnly = true;
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.LoginPath = "/Home/LogIn";
            });
            //services.Configure<SecurityStampValidatorOptions>(options =>
            //    options.ValidationInterval = TimeSpan.FromSeconds(10));
        }

        public static void AddCustomValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AppUserSignInDto>,AppUserSignInValidator>();
            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateValidator>();
            services.AddTransient<IValidator<BlogCreateOrUpdateDto>, BlogCreateOrUpdateValidator>();
            services.AddTransient<IValidator<CategoryBlogAddDto>, CategoryBlogAddValidator>();
            services.AddTransient<IValidator<CategoryCreateOrUpdateDto>, CategoryCreateOrUpdateValidator>();
        }
    }
}
