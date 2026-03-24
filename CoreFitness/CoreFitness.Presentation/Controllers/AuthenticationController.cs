using Application.Abstractions.Identity;
using Application.Members.Abstractions;
using Application.Members.Inputs;
using CoreFitness.Presentation.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

[Route("authentication")]
public class AuthenticationController(IRegisterMemberService registerMemberService, ISignInMemberService signInMemberService, IIdentityService identityService) : Controller

{
    private const string RegistrationEmailSessionKey = "RegistrationEmail";

    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
        return View(new SignInForm());
    }

    [HttpPost("sign-in")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(SignInForm form, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        return View(form);

        var input = new SignInInput(form.Email, form.Password, form.RememberMe);

        var result = await signInMemberService.ExecuteAsync(input, ct);
        if (!result.Success) 
        {
            ViewData["ErrorMessage"] = result.ErrorMessage;
            return View(form);
        }

        return RedirectToAction("My", "Account");
    }

    [HttpPost("log-out")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOut() 
    {
        await identityService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("sign-up")]
    public IActionResult SignUp()
    {
        return View(new RegisterEmailForm());
    }

    [HttpPost("sign-up")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(RegisterEmailForm form, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return View(form);

        HttpContext.Session.SetString(RegistrationEmailSessionKey, form.Email);

        return RedirectToAction(nameof(SetPassword));
    }

    [HttpGet("set-password")]

    public IActionResult SetPassword()
    {
        var email = HttpContext.Session.GetString(RegistrationEmailSessionKey);
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        return View(new RegisterPasswordForm());
    }

    [HttpPost("set-password")]

    public async Task<IActionResult> SetPassword(RegisterPasswordForm form, CancellationToken ct = default)
    {
        try
        {
            var email = HttpContext.Session.GetString(RegistrationEmailSessionKey);
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(nameof(SignUp));

            if (!ModelState.IsValid)
                return View(form);

            var registerMemberInput = new RegisterMemberInput(email, form.Password);
            var registerResult = await registerMemberService.ExecuteAsync(registerMemberInput, ct);
            if (!registerResult.Success)
            {
                return Content(registerResult.ErrorMessage + " | " + registerResult);

                /*ViewData["ErrorMessage"] = registerResult.ErrorMessage;
                return View(form);*/
            }

            var signInMemberInput = new SignInInput(email, form.Password, false);

            var signInResult = await signInMemberService.ExecuteAsync(signInMemberInput, ct);
            if (!signInResult.Success)
            {
                ViewData["ErrorMessage"] = ("The account was created, but sign in failed");
                return View(form);
            }

            HttpContext.Session.Remove(RegistrationEmailSessionKey);
            return RedirectToAction("My", "Account");
        }
        catch (Exception ex) 
        {
            return Content(ex.ToString());
        }
    }
        
}
