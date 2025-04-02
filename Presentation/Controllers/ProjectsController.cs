using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize]
public class ProjectsController : Controller
{

    [Route("admin/projects")]
    public IActionResult Index()
    {

        var projects = new List<Project> {
                new() {Id = 1, Title = "Website Redesign", CompanyName = "GitLab Inc.", Description = "It is necessary to develop a website redesign in a corporate style.", TimeLeft = "1 week left"},
                new() {Id = 2, Title = "Landing Page", CompanyName = "Bitbucket, Inc.", Description = "It is necessary to create a landing together with the development of design.", TimeLeft = "1 week left"},
                new() {Id = 3, Title = "Parser Development", CompanyName = "Driveway, Inc..", Description = "It is necessary to develop a ticket site parser in python.", TimeLeft = "5 days left"},
                new() {Id = 4, Title = "App Development", CompanyName = "Slack Technologies, Inc.", Description = "Create a mobile application on iOS and Android devices.", TimeLeft = "5 days left"},
            };
        return View(projects);
    }
}
