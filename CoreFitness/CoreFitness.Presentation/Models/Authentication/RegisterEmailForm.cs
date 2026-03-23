using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.Authentication;

public class RegisterEmailForm
{
    [Required(ErrorMessage = " Email address is Required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name ="Email Adress", Prompt = "Enter Email Address")]
    public string Email { get; set; } = null!;
}
