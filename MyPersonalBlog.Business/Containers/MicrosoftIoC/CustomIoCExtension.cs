using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyPersonalBlog.Business.Concrete;
using MyPersonalBlog.Business.CustomLogger;
using MyPersonalBlog.Business.Interfaces;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using MyPersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using MyPersonalBlog.DataAccess.Interfaces;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<MyPersonalBlogContext>();

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            //services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EfBlogRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<ICategoryBlogService, CategoryBlogManager>();
            services.AddScoped<ICategoryBlogDal, EfCategoryBlogRepository>();

            services.AddTransient<ICustomLogger, NLogLogger>();
        }
    }
}
