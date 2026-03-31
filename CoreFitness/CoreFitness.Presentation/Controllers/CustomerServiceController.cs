using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class CustomerServiceController : Controller
{
    [HttpGet("support")]
    public IActionResult Support()
    {
        return View();
    }
}
