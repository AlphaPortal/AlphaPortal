using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController(IClientService clientService, IProjectService projectService) : Controller
{
    private readonly IClientService _clientService = clientService;
    private readonly IProjectService _projectService = projectService;

    [Route("admin/[controller]")]
    public async Task<IActionResult> Index()
    {
        var clients = await GetClientsSelectListAsync();
        var result = await _projectService.GetProjectsAsync();
        var projects = result.Result;

        var pvm = new ProjectsViewModel
        {
            Projects = projects,
            AddProjectViewModel = new AddProjectViewModel() { Clients = clients }
        };

        return View(pvm);
    }


    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var addProjectForm = new AddProjectForm
        {
            ProjectName = model.ProjectName,
            Description = model.Description,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            ClientId = model.ClientId,
            Budget = model.Budget,
            UserId = model.UserIds,
        };

        var result = await _projectService.CreateProjectsync(addProjectForm);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        return View(model);
    }


    private async Task<IEnumerable<SelectListItem>> GetClientsSelectListAsync()
    {
        var result = await _clientService.GetClientsAsync();
        if (result.Result != null)
        {
            var statusList = result.Result.Select(s => new SelectListItem
            {
                Value = s.Id,
                Text = s.ClientName,
            });
            return statusList;
        }

        return [];
    }
}
