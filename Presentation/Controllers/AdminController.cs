using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class AdminController : Controller
{
    [HttpGet]
    [Route("admin/members")]
    public IActionResult Members()
    {
        return View();
    }

    [HttpPost]
    [Route("admin/members")]
    public async Task<IActionResult> AddMembers()
    {
        return View();
    }
}
