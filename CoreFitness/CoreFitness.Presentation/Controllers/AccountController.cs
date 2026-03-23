using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

[Authorize]
[Route("accout")]
public class AccountController : Controller
{
    public IActionResult SignIn()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpGet("My")]
    public IActionResult My()
    {
        return View();
    }
}