using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class MembersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
