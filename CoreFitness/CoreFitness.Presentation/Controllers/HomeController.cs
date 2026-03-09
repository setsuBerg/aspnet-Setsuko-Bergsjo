using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {

        return View();
    }
}
