using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces;

public interface IAuthService
{
    Task<bool> SignInAsync(string email, string password, bool rememberMe = false);
    Task<IdentityResult> SignUpAsync(SignUpDto dto, string roleName = "User");
    Task<bool> UserExistsAsync(string email);
    Task SignOutAsync();
}
