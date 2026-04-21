using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class ErrorsController : Controller
{
    [HttpGet("/Errors/NotFoundPage")]
    public IActionResult NotFoundPage()
    {
        return View();
    }
}
