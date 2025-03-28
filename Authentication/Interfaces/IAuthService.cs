using Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Interfaces;

public interface IAuthService
{
    Task<SignInResult> SignInAsync(SignInForm form);
}
