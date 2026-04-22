using Domain.Aggregates.Members;
namespace Tests;

public class MemberTests
{
    [Fact]
    public void Create_should_throw_exception_when_userId_is_empty()
    {
        //Act & Assert
        Assert.Throws<ArgumentException>(static () => Member.Create(""));
    }

    [Fact]
    public void UpdateInformation_should_update_values_correctly()
    {
        //Arrange
        var member = Member.Create("user1");

        //Act
        member.UpdateInformation("Hanna", "Suzuki", "0700704444", null);

        //Assert
        Assert.Equal("Hanna", member.FirstName);
        Assert.Equal("Suzuki", member.LastName);
        Assert.Equal("0700704444", member.PhoneNumber);
    }
}
