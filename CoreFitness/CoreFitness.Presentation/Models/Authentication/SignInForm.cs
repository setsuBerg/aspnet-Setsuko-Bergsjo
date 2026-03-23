using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.Authentication;

public class SignInForm
{
    [Required(ErrorMessage = " Email address is Required")]
    [Display(Name = "Email Adress", Prompt = "Enter Email Address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = " Password is Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Password")]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}