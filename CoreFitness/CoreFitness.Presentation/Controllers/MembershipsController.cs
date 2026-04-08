using Application.Memberships;
using CoreFitness.Presentation.Models.Memberships;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Presentation.Controllers;

public class MembershipsController(IMembershipService service, DataContext context) : Controller
{
    public async Task<IActionResult> Index()
    {

        var memberships = await service.GetMembershipsAsync();

        var viewModel = new MembershipViewModel()
        {
            Memberships = memberships.OrderBy(x => x.Price).ToList() 
        };

        var faqs = await context.Faqs.ToListAsync();

        viewModel.Faqs = faqs.Select(x => new FaqItemViewModel
        {
            Title = x.Title,
            Description = x.Description
        }).ToList();

        return View(viewModel);
    }
}
