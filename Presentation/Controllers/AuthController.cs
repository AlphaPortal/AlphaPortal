using Authentication.Interfaces;
using Authentication.Models;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Security.Claims;

namespace Presentation.Controllers;

public class AuthController(IAuthService authService, INotificationService notificationService, IUserService userService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
{

    private readonly IAuthService _authService = authService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IUserService _userService = userService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly UserManager<AppUser> _userManager = userManager;

    [Route("auth/login")]
    public IActionResult SignIn(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "";
        return View();
    }

    [HttpPost]
    [Route("auth/login")]
    public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = "~/")
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.SignInAsync(model.Email, model.Password, model.RememberMe);
            if (result)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userResult = await _userService.GetUserByIdAsync(userId!);
                var user = userResult.Result;
                var notificationFormData = new NotificationItemForm
                {
                    NotificationTargetId = 1,
                    NotificationTypeId = 1,
                    Message = $"{user!.FirstName} {user.LastName}",
                    Image = user.ImageUrl,
                };
                await _notificationService.AddNotificationAsync(notificationFormData);

                return LocalRedirect(returnUrl);
            }
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Unable to sign in. Try another email or password";
        return View(model);
    }


    public IActionResult SignUp(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "";
        return View();
    }

    [Route("auth/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model, string returnUrl = "~/")
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ErrorMessage = "";
            return View(model);
        }

        if (await _authService.UserExistsAsync(model.Email))
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ErrorMessage = "User already exists.";
            return View(model);
        }

        var dto = new SignUpDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
        };

        var result = await _authService.SignUpAsync(dto);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.ErrorMessage = "Something went wrong. Try again later.";
        return View(model);
    }

    [Route("auth/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _authService.SignOutAsync();
        return LocalRedirect("~/");
    }

    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Invalid provider");
            return View("SignIn");
        }

        var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("SignIn");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction("SignIn");
        }

        var signinResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signinResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            string firstname = string.Empty;
            string lastname = string.Empty;

            try
            {
                firstname = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
                lastname = info.Principal.FindFirstValue(ClaimTypes.Surname)!;
            }
            catch (Exception ex)
            {

            }

            string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{info.LoginProvider.ToLower()}_{email}";

            var user = new AppUser
            {
                UserName = username,
                Email = email,

                Profile = new AppUserProfile
                {
                    FirstName = firstname,
                    LastName = lastname,
                    ImageUrl = "avatar_2.svg",
                }
            };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("SignIn");
        }
    }
}
