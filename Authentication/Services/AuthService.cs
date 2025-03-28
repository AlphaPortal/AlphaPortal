using Authentication.Interfaces;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;
public class AuthService(SignInManager<AppUser> signInManager) : IAuthService
{

    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public async Task<bool> SignInAsync(SignInForm form)
    {
        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, false);
        return result.Succeeded;
    }
}
