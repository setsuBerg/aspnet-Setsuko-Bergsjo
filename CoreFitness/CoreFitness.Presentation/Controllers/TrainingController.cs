using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class TrainingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
