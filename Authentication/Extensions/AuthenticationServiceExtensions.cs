using Authentication.Contexts;
using Authentication.Interfaces;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddLocalAuthentication(this IServiceCollection services, string connectionString, string loginPath = "/auth/login", string accessDenidePath = "/denide")
    {
        services.AddDbContext<AuthenticationContext>(x => x.UseSqlServer(connectionString));
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AuthenticationContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = loginPath;
            options.AccessDeniedPath = accessDenidePath;
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
