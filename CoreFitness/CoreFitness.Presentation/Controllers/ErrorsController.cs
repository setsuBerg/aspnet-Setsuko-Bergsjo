using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class ErrorsController : Controller
{
    public IActionResult NotFoundPage()
    {
        return View();
    }
}
