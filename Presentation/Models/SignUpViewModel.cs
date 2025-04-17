using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class SignUpViewModel
{
    [Required(ErrorMessage = "is required")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter first name")]
    public string FirstName { get; set; } = null!;


    [Required(ErrorMessage = "is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;


    [Required(ErrorMessage = "is required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "You must enter a strong password.")]
    public string Password { get; set; } = null!;


    [Required(ErrorMessage = "is required")]
    [Compare(nameof(Password), ErrorMessage = "Password must be confirmed.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm password")]
    public string ConfirmPassword { get; set; } = null!;
}
