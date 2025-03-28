using Authentication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Globalization;

namespace Authentication.Extensions;

public static class ApplicationDefaultAdminUserExtensions
{
    public static IApplicationBuilder UseDefaultAdminAccount(this IApplicationBuilder app, string email = "admin@domain.com", string password = "BytMig123!", string role = "admin")
    {
        return app.UseMiddleware<DefaultAdminAccountMiddleware>(email, password, role);
    }

    public class DefaultAdminAccountMiddleware(RequestDelegate next, string email, string password, string role)
    {
        private readonly RequestDelegate _next = next;
        private readonly string _email = email;
        private readonly string _password = password;
        private readonly string _role = role;

        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync(_email);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = _email,
                    Email = _email,
                };

                adminUser.Profile = new AppUserProfile
                {
                    UserId = adminUser.Id
                };

                adminUser.Address = new AppUserAddress
                {
                    UserId = adminUser.Id,
                };

                var result = await userManager.CreateAsync(adminUser, _password);
                if (result.Succeeded)
                {
                    if (_role != null)
                    {
                        await userManager.AddToRoleAsync(adminUser, _role);
                    }
                }
            }

            await _next(context);
        }
    }
}



