using Application.Abstractions.Identity;
using Application.Common.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    public async Task<Result<string?>> CreateUserAsync(string email, string password, CancellationToken ct = default)
    {
        var exisingUser = await userManager.FindByEmailAsync(email);
        if (exisingUser is not null)
            return Result<string?>.Conflict("An account with this email already exists.");

        var user = ApplicationUser.Create(email);

        var result = await userManager.CreateAsync(user, password);
        return !result.Succeeded ? Result<string?>.Error() : Result<string?>.Ok(user.Id);

    }

    public async Task<Result<bool>> PasswordSignInAsync(string email, string password, bool rememberMe, CancellationToken ct = default)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return !result.Succeeded ? Result<bool>.Error("Invalid email or password") : Result<bool>.Ok(true);
    }

    public Task SignOutAsync(CancellationToken ct = default)
    {
        return signInManager.SignOutAsync();
    }
}
