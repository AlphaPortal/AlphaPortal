using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
    public AddProjectViewModel AddProjectViewModel { get; set; } = new AddProjectViewModel();
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
}
