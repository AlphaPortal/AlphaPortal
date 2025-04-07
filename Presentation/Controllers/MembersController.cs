using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
[Route("admin/[controller]")]
public class MembersController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}
