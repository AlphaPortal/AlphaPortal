using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController(IClientService clientService) : Controller
{
    private readonly IClientService _clientService = clientService;

    [Route("admin/projects")]
    public async Task<IActionResult> Index()
    {
        var clients = await GetClientsSelectListAsync();

        var projects = new List<Project> {
                new() {Id = 1, Title = "Website Redesign", Description = "GitLab Inc.", Message = "It is necessary to develop a website redesign in a corporate style.", TimeLeft = "1 week left"},
                new() {Id = 2, Title = "Landing Page", Description = "Bitbucket, Inc.", Message = "It is necessary to create a landing together with the development of design.", TimeLeft = "1 week left"},
                new() {Id = 3, Title = "Parser Development", Description = "Driveway, Inc..", Message = "It is necessary to develop a ticket site parser in python.", TimeLeft = "5 days left"},
                new() {Id = 4, Title = "App Development", Description = "Slack Technologies, Inc.", Message = "Create a mobile application on iOS and Android devices.", TimeLeft = "5 days left"},
                new() {Id = 5, Title = "Website Redesign", Description = "GitLab Inc.", Message = "It is necessary to develop a website redesign in a corporate style.", TimeLeft = "1 week left"},
                new() {Id = 6, Title = "Landing Page", Description = "Bitbucket, Inc.", Message = "It is necessary to create a landing together with the development of design.", TimeLeft = "1 week left"},
                new() {Id = 7, Title = "Parser Development", Description = "Driveway, Inc..", Message = "It is necessary to develop a ticket site parser in python.", TimeLeft = "5 days left"},
                new() {Id = 8, Title = "App Development", Description = "Slack Technologies, Inc.", Message = "Create a mobile application on iOS and Android devices.", TimeLeft = "5 days left"},
            };

        var pvm = new ProjectsViewModel
        {
            Projects = projects,
            AddProjectViewModel = new AddProjectViewModel() { CLients = clients }
        };
            
        return View(pvm);
    }

    [HttpPost]
    public async Task<IActionResult> Add()
    {
        var viewModel = new AddProjectViewModel
        {
            CLients = await GetClientsSelectListAsync()
        };

        return PartialView("~/Views(Shared/Modals(_AddProjectModal", viewModel);
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
