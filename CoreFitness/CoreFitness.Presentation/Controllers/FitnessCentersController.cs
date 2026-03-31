using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class FitnessCentersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
