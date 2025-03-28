using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces;

public interface IAuthService
{
    Task<bool> SignInAsync(SignInForm form);
}
