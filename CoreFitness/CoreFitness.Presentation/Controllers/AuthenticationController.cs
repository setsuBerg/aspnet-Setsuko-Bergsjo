using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class AuthenticationController : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }
    public IActionResult RegisterEmail()
    {
        return View();
    }
    public IActionResult RegisterPassword()
    {
        return View();
    }
    public IActionResult RegisterProfile()
    {
        return View();
    }
}
