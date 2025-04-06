using Business.Models;

namespace Presentation.Models;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = [];
    public AddProjectViewModel AddProjectViewModel { get; set; } = new AddProjectViewModel();
}
