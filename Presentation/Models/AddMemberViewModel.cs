using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class AddMemberViewModel
{
    [DataType(DataType.Upload)]
    [Display(Name = "Project Image", Prompt = "Select project image")]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phone number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Text)]
    [Display(Name = "Job Title", Prompt = "Enter job title")]
    public string? JobTitle { get; set; }
}
