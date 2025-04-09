using Business.Models;
using Data.Entities;
using Domain.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(AddProjectForm project)
    {
        var entity = new ProjectEntity
        {
            Image = project.Image,
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
                Image = clientEntity.Image,
                ClientName = clientEntity.ClientName,
            },

            User = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JobTitle = user.JobTitle,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                StreetName = user.StreetName,
                PostalCode = user.PostalCode,
                City = user.City,
                Country = user.Country,
            }
        };

        return project;
    }
}