using Application.Common.Results;
using Application.Members.Abstractions;
using Application.Members.Inputs;
using Domain.Abstractions.Repositories.Members;
using Domain.Aggregates.Members;

namespace Application.Members.Services;

public class UpdateMemberProfileService(IMemberRepository memberRepository) : IUpdateMemberProfileService
{
    public async Task<Result<Member>> ExecuteAsync(UpdateMemberProfileInput input, CancellationToken ct = default)
    {
        try
        {
            if (input is null)
                throw new ArgumentException("Input must be provided");

            var member = await memberRepository.GetMemberByUserIdAsync(input.UserId, ct);
            if (member is null)
                return Result<Member>.NotFound($"Member width user id'{input.UserId}' was not found");

            member.UpdateInformation(input.FirstName, input.LastName, input.PhoneNumber, input.ProfileImageUri);
            var result = await memberRepository.UpdateAsync(member, ct);

            return !result
                ? Result<Member>.Error($"Member width user id'{member.UserId}' was not updated")
                : Result<Member>.Ok(member);

        }
        catch (Exception ex)
        {
            return Result<Member>.Error(ex.Message);
        }
    }
}

