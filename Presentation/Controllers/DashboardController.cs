using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    public class DashboardController : Controller
    {
        //[Route("admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
