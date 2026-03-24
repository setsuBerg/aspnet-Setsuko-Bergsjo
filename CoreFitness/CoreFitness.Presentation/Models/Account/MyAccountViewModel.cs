using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models.Account;

public class MyAccountViewModel
{
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;
    public MyProfileForm AboutMeForm { get; set; } = new();
}

