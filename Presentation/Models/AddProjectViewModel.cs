﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AddProjectViewModel
{
    [Required]
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image", Prompt = "Select project image")]
    public IFormFile? ImageUrl { get; set; }
    public string Image { get; set; } = "project_image_9";
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Project Name", Prompt = "Enter project name")]
    public string ProjectName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Start Date", Prompt = "Enter start date")]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date", Prompt = "Enter end date")]
    public DateTime? EndDate { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Budget", Prompt = "Enter budget")]
    public decimal? Budget { get; set; }

    [Required]
    [Display(Name = "Client", Prompt = "Select client")]
    public string ClientId { get; set; } = null!;

    [Display(Name = "Members", Prompt = "Search for members")]
    public string? UserIds { get; set; }
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
}
