using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, IUserRepository userRepository, IUserService userService, IClientService clientService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;
    private readonly IClientService _clientService = clientService;

    public async Task<ProjectResult> CreateProjectsync(AddProjectForm projectForm)
    {
        if (projectForm != null)
        {
            var entity = ProjectFactory.Create(projectForm);
            var result = await _projectRepository.AddAsync(entity);
            if (result != null)
            {
                return new ProjectResult { Succeeded = true, StatusCode = 200 };
            }

            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = result!.Error };
        }

        return new ProjectResult { Succeeded = false, StatusCode = 500, Error = "Something went wrong" };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = new List<Project>();

        foreach (var entity in entities.Result!)
        {
            var user = await _userService.GetUserByIdAsync(entity.UserId);
            var client = await _clientService.GetClientByIdAsync(entity.ClientId);

            var projectUser = user.Result;
            if (projectUser != null)
            {
                var projectClient = client.Result;

                if (projectClient != null)
                {
                    projects.Add(ProjectFactory.Create(projectUser, entity, projectClient));
                }
            }

        }

        return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = projects };
    }
}