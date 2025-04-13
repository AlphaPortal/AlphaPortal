using Business.Models;
using Data.Entities;
using Domain.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(AddProjectForm project)
    {
        var entity = new ProjectEntity
        {
            Image = project.ImageUrl,
            ProjectName = project.ProjectName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            ClientId = project.ClientId,
            UserId = project.UserId!
        };

        return entity;
    }

    public static ProjectEntity Create(Project project)
    {
        var entity = new ProjectEntity
        {
            Id = project.Id!,
            Image = project.Image,
            ProjectName = project.ProjectName,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            Created = project.Created,
            StatusId = project.StatusId,
            ClientId = project.ClientId,
            UserId = project.UserId!
        };

        return entity;
    }

    public static Project Create(User user, ProjectEntity projectEntity, Client clientEntity)
    {
        var project = new Project
        {
            Id = projectEntity.Id,
            Image = projectEntity.Image,
            ProjectName = projectEntity.ProjectName,
            Description = projectEntity.Description,
            StartDate= projectEntity.StartDate,
            EndDate = projectEntity.EndDate,
            Budget = projectEntity.Budget,
            Created = projectEntity.Created,
            StatusId = projectEntity.StatusId,
            ClientId= projectEntity.ClientId,
            UserId = user.Id,

            Client = new Client
            {
                Id= clientEntity.Id,
                Image = clientEntity.Image,
                ClientName = clientEntity.ClientName,
            },

            User = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JobTitle = user.JobTitle,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
                StreetName = user.StreetName,
                PostalCode = user.PostalCode,
                City = user.City,
                Country = user.Country,
            }
        };

        return project;
    }

    public static Project Create(ProjectEntity entity)
    {
        var project = new Project
        {
            Id = entity.Id,
            Image = entity.Image,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Created = entity.Created,
            StatusId = entity.StatusId,
            ClientId = entity.ClientId,
            UserId = entity.UserId,

            //Client = new Client
            //{
            //    Image = entity.Client.Image,
            //    ClientName = entity.Client.ClientName,
            //}
        };

        return project;
    }
}