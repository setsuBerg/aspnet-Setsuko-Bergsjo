using Application.Abstractions.Identity;
using Application.Members.Abstractions;
using Application.Members.Inputs;
using CoreFitness.Presentation.Models.Account;
using Infrastructure.Identity;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

[Authorize]
[Route("account")]
public class AccountController
    (
        DataContext context,
        UserManager<ApplicationUser> userManager,
        IGetMemberProfileService getMemberProfileService,
        IUpdateMemberProfileService updateMemberProfileService,
        IIdentityService identityService
    ) : Controller
{
    [HttpGet("my")]
    public async Task<IActionResult> My(CancellationToken ct = default)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
            return Challenge();

        var profile = await getMemberProfileService.ExecuteAsync(user.Id, ct);
        if (profile is null)
            return NotFound();

        var viewModel = new MyAccountViewModel
        {
            Email = user.Email ?? string.Empty,
            AboutMeForm = new MyProfileForm
            {
                FirstName = profile.Value?.FirstName ?? string.Empty,
                LastName = profile.Value?.LastName ?? string.Empty,
                PhoneNumber = profile.Value?.PhoneNumber ?? string.Empty,
                ProfileImageUri = profile.Value?.ProfileImageUri ?? string.Empty
            }
        };

        return View(viewModel);
    }

    [HttpPost("my")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> My(MyAccountViewModel viewModel, CancellationToken ct = default)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null)
            return Challenge();

        if (!ModelState.IsValid)
            return View(viewModel);

        viewModel.Email = user.Email ?? string.Empty;

        var input = new UpdateMemberProfileInput
            (
                user.Id,
                viewModel.AboutMeForm.FirstName,
                viewModel.AboutMeForm.LastName,
                viewModel.AboutMeForm.PhoneNumber,
                viewModel.AboutMeForm.ProfileImageUri
            );

        var result = await updateMemberProfileService.ExecuteAsync(input, ct);
        if (!result.Success) 
        {
            ViewData["Message"] = result.ErrorMessage;
            ViewData["MessageType"] = "error";
            return View(viewModel);
        }

        ViewData["Message"] = result.ErrorMessage;
        ViewData["MessageType"] = "success";

        return View(viewModel);
    }

    [HttpPost("remove-account")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveAccount() 
    {
        var user = await userManager.GetUserAsync (User);
        if (user is null)
            return Challenge();

        await userManager.DeleteAsync (user);
        await identityService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("bookings")]
    public IActionResult Bookings()
    {
        var userId = userManager.GetUserId(User);
        if (userId is null)
            return Challenge();

        var bookings = context.Bookings
            .Where(x => x.UserId == userId)
            .Join(context.TrainingClasses,
                  booking => booking.TrainingClassId,
                  trainingClass => trainingClass.Id,
                  (booking, trainingClass) => new { Booking = booking, TrainingClass = trainingClass })
            .ToList();

        return View(bookings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CancelBooking(string id)
    {
        var userId = userManager.GetUserId(User);
        if (userId is null)
            return Challenge();
        var booking = context.Bookings.FirstOrDefault(x => x.Id == id && x.UserId == userId);
        if (booking is null)
            return NotFound();
        context.Bookings.Remove(booking);
        context.SaveChanges();
        return RedirectToAction("Bookings");
    }



    [HttpGet("membership")]
    public IActionResult Membership()
    {
        
        return View();
    }
}