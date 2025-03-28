using Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {

        private readonly IAuthService _authService = authService;

        [Route("auth/signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("auth/signin")]
        public IActionResult SignIn(SignInViewModel model)
        {


            return View();
        }

        [Route("auth/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("auth/logout")]
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
