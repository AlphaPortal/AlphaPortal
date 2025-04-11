using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResult> CreateProjectsync(AddProjectForm project);
        Task<ProjectResult<Project>> GetProjectAsync(string id);
        Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync(int status);
        Task<ProjectResult> RemoveProjectAsync(string id);
        Task<ProjectResult> UpdateProjectAsync(Project project);
    }
}