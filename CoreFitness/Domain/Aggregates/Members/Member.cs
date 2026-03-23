namespace Domain.Aggregates.Members;

public class Member
{
    public string Id { get; private set; } = null!;
    public string UserId { get; private set; } = null!;

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ProfileImageUri { get; private set; }
    public DateTimeOffset CreateAt { get; private set; }
    public DateTimeOffset? ModifiedAt { get; private set; }

    private Member() 
    {

    }
    private Member(string id, string userId, DateTimeOffset createAt)
    {
        Id = id;
        UserId = userId;
        CreateAt = createAt;
    }

    public static Member Create(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("Application User id is required.");

        var member = new Member(
            Guid.NewGuid().ToString(), 
            userId, 
            DateTimeOffset.UtcNow
        );

        return member;
    }

    public static Member Create(string id, string userId, string? firstName, string? LastName, string? phoneNumber, string? profileImageUri, DateTimeOffset createdAt, DateTimeOffset? modifiedAt)
    {

        var member = new Member(id, userId, createdAt)
        {
            FirstName = firstName,
            LastName = LastName,
            PhoneNumber = phoneNumber,
            ProfileImageUri = profileImageUri,
            ModifiedAt = modifiedAt
        };

        return member;
    }

    public void UpdateInformation(string firstName, string lastName, string phoneNumber, string? profileImageUri)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;
        ProfileImageUri = string.IsNullOrWhiteSpace(profileImageUri) ? null : profileImageUri;
    }
}
