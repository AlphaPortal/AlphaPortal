using Authentication.Interfaces;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;
public class AuthService(SignInManager<AppUser> signInManager) : IAuthService
{

    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<bool> SignInAsync(string email, string password, bool rememberMe = false)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return result.Succeeded;
    }
}
