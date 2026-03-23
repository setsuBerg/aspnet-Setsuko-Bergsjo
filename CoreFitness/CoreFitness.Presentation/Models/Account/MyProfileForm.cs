using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.Account;

public class MyProfileForm
{
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name", Prompt = "First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name", Prompt = "Last Name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Phone Number", Prompt = "Enter Phone Number")]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Profile Image", Prompt = "Upload Profile Image")]
    public string? ProfileImageUri { get; set; }
}

