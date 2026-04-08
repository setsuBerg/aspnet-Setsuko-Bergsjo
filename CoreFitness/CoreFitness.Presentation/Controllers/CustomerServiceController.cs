using AspNetCoreGeneratedDocument;
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
        var viewModel = new MembershipViewModel();

        viewModel.Faqs = faqs.Select(x => new FaqItemViewModel
        {
            Title = x.Title,
            Description = x.Description
        }).ToList();

        return View(viewModel);
    }
}