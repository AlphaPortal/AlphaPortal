using Business.Interfaces;
using Business.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController(IClientService clientService, IProjectService projectService, INotificationService notificationService, IWebHostEnvironment env) : Controller
{
    private readonly IClientService _clientService = clientService;
    private readonly IProjectService _projectService = projectService;
    private readonly INotificationService _notificationService = notificationService;
    private readonly IWebHostEnvironment _env = env;

    [Route("admin/[controller]")]
    public async Task<IActionResult> Index(int status)
    {
        var clients = await GetClientsSelectListAsync();
        var result = await _projectService.GetProjectsAsync(status);
        var projects = result.Result;

        ViewBag.Status = status;

        var pvm = new ProjectsViewModel
        {
            Projects = projects!,
            AddProjectViewModel = new AddProjectViewModel() { Clients = clients },
        };

        return View(pvm);
    }


    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel model)
    {
        if (!ModelState.IsValid || model.ImageUrl == null || model.ImageUrl.Length == 0)
        {
            return View(model);
        }

        var uploadFolder = Path.Combine(_env.WebRootPath, "Images/Projects");
        Directory.CreateDirectory(uploadFolder);

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.ImageUrl.FileName)}";
        var filePath = Path.Combine(uploadFolder, fileName);

        using (var stram = new FileStream(filePath, FileMode.Create))
        {
            await model.ImageUrl.CopyToAsync(stram);
        }

        ViewBag.Message = "File was uploaded siccwssfully.";

            var addProjectForm = new AddProjectForm
            {
                ImageUrl = fileName,
                ProjectName = model.ProjectName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ClientId = model.ClientId,
                Budget = model.Budget,
                UserId = model.UserIds,
            };

        var result = await _projectService.CreateProjectsync(addProjectForm);
        if (result.Succeeded)
        {
            var notificationFormData = new NotificationItemForm
            {
                NotificationTargetId = 1,
                NotificationTypeId = 2,
                Message = $"Added {model.ProjectName}",
                Image = model.Image,
            };
            await _notificationService.AddNotificationAsync(notificationFormData);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditProject(Project project)
    {
        var result = await _projectService.UpdateProjectAsync(project);
        if (result.Succeeded)
        {
            var notificationFormData = new NotificationItemForm
            {
                NotificationTargetId = 1,
                NotificationTypeId = 2,
                Message = $"Project ({project.ProjectName})  Updated",
                Image = project.Image,
            };
            return RedirectToAction("Index");
        }

        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProject(string id)
    {
        var result = await _projectService.RemoveProjectAsync(id);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        return View(id);
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
