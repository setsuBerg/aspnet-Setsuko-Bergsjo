using Application.Abstractions.Identity;
using Application.Common.Results;
using Application.Members.Abstractions;
using Application.Members.Inputs;
using Domain.Abstractions.Repositories.Members;
using Domain.Aggregates.Members;

namespace Application.Members.Services;

public class SignInMemberService(IIdentityService identityService) : ISignInMemberService
{
    public async Task<Result> ExecuteAsync(SignInInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                throw new ArgumentException("Input must be provided");

            var result = await identityService.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, ct);
            return !result.Success ? Result.Error(result.ErrorMessage ?? "Invalid email or password") : Result.Ok();

        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
