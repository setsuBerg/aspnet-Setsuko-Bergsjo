using Application.Memberships;
using CoreFitness.Presentation.Models.Memberships;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class MembershipsController(IMembershipService service) : Controller
{
    public async Task<IActionResult> Index()
    {

        var memberships = await service.GetMembershipsAsync();

        var viewModel = new MembershipViewModel()
        {
            Memberships = memberships.OrderBy(x => x.Price).ToList() 
        };

        return View(viewModel);
    }
}
