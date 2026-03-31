using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class StoreController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
