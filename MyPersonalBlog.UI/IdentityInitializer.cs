using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyPersonalBlog.Entities.Concrete;

namespace MyPersonalBlog.UI
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin"
                });
            }

            var adminUser = await userManager.FindByNameAsync("denemehesap123");
            if (adminUser == null)
            {
                AppUser user = new AppUser()
                {
                    Name = "Ramazan",
                    Surname = "İşçanan",
                    UserName = "denemehesap123",
                    Email = "iscananramazan@gmail.com"
                };
                await userManager.CreateAsync(user, "denemehesap123.");//user'ı ekle parolasınıda 1 yap'
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
