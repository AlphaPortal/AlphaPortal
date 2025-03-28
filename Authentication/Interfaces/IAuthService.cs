using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces;

public interface IAuthService
{
    Task<bool> SignInAsync(string email, string password, bool rememberMe = false);
}
