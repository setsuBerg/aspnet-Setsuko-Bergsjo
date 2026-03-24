using Infrastructure.Persistence.Entities.Members;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public MemberEntity? Member { get; set; }

    public static ApplicationUser Create(string email, bool confirmed = true) 
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException("Email is required");

        var user = new ApplicationUser 
        { 
            UserName = email,
            Email = email,
            EmailConfirmed = confirmed
        };

        return user;
    }
}
