using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using MyPersonalBlog.Business.Containers.MicrosoftIoC;
using MyPersonalBlog.Entities.Concrete;
using MyPersonalBlog.UI.CustomCollectionExtensions;
using MyPersonalBlog.UI.Tools.AppClasses;

namespace MyPersonalBlog.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddDependencies();
            services.AddCustomIdentity();
            services.AddAutoMapper(typeof(Startup));
            services.AddCustomValidator();
            services.AddControllersWithViews().AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/ErrorPage");
            }

            app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?code = {0}");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            IdentityInitializer.SeedData(userManager, roleManager).Wait();
            NLogConfiguration.ConfigurationForNLog(env);
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "normal",
                    pattern: "{controller}/{action=LogIn}/{id?}"
                     );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Member}/{controller=Home}/{action=Index}/{id?}"
                     );
            });
        }
    }
}
