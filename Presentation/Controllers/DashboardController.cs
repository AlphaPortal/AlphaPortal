using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class DashboardController : Controller
{
    [Route("admin/dashboard")]
    public IActionResult Index()
    {
        return View();
    }
}
