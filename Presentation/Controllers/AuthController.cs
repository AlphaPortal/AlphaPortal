﻿using Authentication.Interfaces;
using Authentication.Models;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthController(IAuthService authService, INotificationService notificationService, IUserService userService) : Controller
    {

        private readonly IAuthService _authService = authService;
        private readonly INotificationService _notificationService = notificationService;
        private readonly IUserService _userService = userService;

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
                        Message = $"Björn Åhström",
                        Image = "user.Image",
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
    }
}
