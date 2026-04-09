using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.CustomerService;

public class ContactFormModel
{
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name", Prompt = "Enter First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name", Prompt = "Enter Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email Address", Prompt = "Enter Email Address")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone Number", Prompt = "Enter Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [Display(Name = "Message", Prompt = "Message...")]
    public string Message { get; set; } = string.Empty;
}

