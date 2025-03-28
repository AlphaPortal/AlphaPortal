using Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {

        private readonly IAuthService _authService = authService;

        [Route("auth/signin")]
        public IActionResult SignIn(string returnUrl = "~/")
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        [Route("auth/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.SignInAsync(model.Email, model.Password, model.RememberMe);
                if (result)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ErrorMessage = "Unable to sign in. Try another email or password";
            return View(model);
        }

        //[Route("auth/signup")]
        //public IActionResult SignUp()
        //{
        //    return View();
        //}

        [Route("auth/logout")]
        public async Task<IActionResult> LogOut()
        {
            await _authService.SignOutAsync();
            return View();
        }
    }
}
