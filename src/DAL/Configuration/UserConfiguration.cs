using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class UserConfiguration
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("grinch.in.ua@ukr.net").Result == null)
            {
                var user = new User
                {
                    FirstName = "Ярослав",
                    LastName = "Макітра",
                    Email = "grinch.in.ua@ukr.net",
                    NormalizedEmail = "GRINCH.IN.UA@UKR.NET",
                    UserName = "grinch.in.ua@ukr.net",
                    NormalizedUserName = "GRINCH.IN.UA@UKR.NET",
                    EmailConfirmed = true,
                    PhoneNumber = "+380983839980"
                };

                IdentityResult result = userManager.CreateAsync(user, "Mulik@1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
                
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                _ = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
