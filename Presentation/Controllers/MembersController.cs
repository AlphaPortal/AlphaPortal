using Authentication.Contexts;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers;

[Authorize]
public class MembersController : Controller
{
    private readonly IUserService _userService;
    private readonly AuthenticationContext _context;

    public MembersController(IUserService userService, AuthenticationContext authenticationContext)
    {
        _userService = userService;
        _context = authenticationContext;
    }

    [Route("admin/[controller]")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> SearchUsers(string term)
    {
        if(string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }

        var users = await _context.Users
            .Include(u => u.Profile)
            .Where(u => u.Profile!.FirstName!.Contains(term) || u.Profile.LastName!.Contains(term) || u.Email!.Contains(term))
            .Select(u => new {u.Id, u.Profile!.ImageUrl, FullName = $"{u.Profile.FirstName} {u.Profile.LastName}"})
            .ToListAsync();

        return Json(users);
    }
}
