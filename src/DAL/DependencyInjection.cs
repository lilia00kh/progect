using System;
using DAL.EF;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataProtection().PersistKeysToDbContext<ApplicationDbContext>();
                    services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireUppercase = true;
                    opt.User.RequireUniqueEmail = true;

                    opt.SignIn.RequireConfirmedEmail = true;
                })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProdactionConnection")));
            services.BuildServiceProvider().GetService<ApplicationDbContext>().Database.Migrate();
            return services;
        }
    }
}
