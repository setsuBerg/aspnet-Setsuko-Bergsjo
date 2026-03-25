using Application.Abstractions.Identity;
using Application.Members.Abstractions;
using Application.Members.Inputs;
using CoreFitness.Presentation.Models.Account;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

[Authorize]
[Route("account")]
public class AccountController
    (
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
}