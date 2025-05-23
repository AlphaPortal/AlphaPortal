﻿using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using System.Linq.Expressions;

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
            if(projectForm.ImageUrl == null)
            {
                projectForm.ImageUrl = "project_image_9.svg";
            }

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


    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync(int status = 0)
    {

        // Took help from ChatGpt handle status
        Expression<Func<ProjectEntity, bool>>? filter = status > 0 ? p => p.StatusId == status : null;

            var entities = await _projectRepository.GetAllAsync
            (
                orderByDescending: false,
                sortByColumn: p => p.ProjectName,
                filterBy: filter
            );

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

    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var result = await _projectRepository.GetAsync(p => p.Id == id);
        if (result.Succeeded)
        {
           if (result.Result != null)
            {
                var project = ProjectFactory.Create(result.Result);
                return new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = project };
            }

            return new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = result.Error };
        }

        return new ProjectResult<Project> { Succeeded = false, StatusCode = 400, Error = result.Error };
    }

    public async Task<ProjectResult> UpdateProjectAsync(Project project)
    {
        if (project != null)
        {
            var entity = ProjectFactory.Create(project);
            var result = await _projectRepository.UpdateAsync(entity);
            if (result.Succeeded)
            {
                return new ProjectResult { Succeeded = true, StatusCode = 200 };
            }
            else
            {
                return new ProjectResult { Succeeded = true, StatusCode = 404, Error = result.Error };
            }
        }

        return new ProjectResult { Succeeded = true, StatusCode = 400, Error = "Something wen wrong"};

    }

    public async Task<ProjectResult> RemoveProjectAsync(string id)
    {
        var result = await _projectRepository.DeleteAsync(p => p.Id == id);
        if (result.Succeeded)
        {
            return new ProjectResult { Succeeded = true, StatusCode = 200 };
        }

        return new ProjectResult { Succeeded = false, StatusCode = 400, Error = result.Error };
    }
}