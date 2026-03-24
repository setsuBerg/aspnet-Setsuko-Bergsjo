using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.Authentication;

public class RegisterPasswordForm
{
    [Required(ErrorMessage = " Password is Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Password")]
    public string Password { get; set; } = null!;


    [Required(ErrorMessage = " You must confirm your Password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;
}
