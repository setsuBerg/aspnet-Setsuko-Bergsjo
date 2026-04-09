using CoreFitness.Presentation.Models.CustomerService;
using CoreFitness.Presentation.Models.Memberships;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Presentation.Controllers;

public class CustomerServiceController(DataContext context) : Controller
{
    [HttpGet("support")]
    public async Task<IActionResult> Support() 
    {
        var faqs = await context.Faqs.ToListAsync();
        var viewModel = new CustomerServiceViewModel();

        viewModel.Faqs = faqs.Select(x => new FaqItemViewModel
        {
            Title = x.Title,
            Description = x.Description
        }).ToList();

        return View(viewModel);
    }

    [HttpPost("support")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Support(CustomerServiceViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Success"] = "Message sent successfully!";
        return RedirectToAction("Support");
    }
}