using Microsoft.Extensions.DependencyInjection;
using MYAPI.Services;
using MYAPI.Tests.Integration;

namespace MYAPI.Integration.Tests.Services;

public class UserServiceTests : IntegrationBase
{
    private readonly IUserService _sut;

    public UserServiceTests(SystemDescriptor factory)
        : base(factory)
    {
        _sut = factory.Provider.GetService<IUserService>();
    }

    [Fact]
    public async Task Get_should_take_from_database()
    {
        var email = "asd@asd.com";
        var username = "username";
        var gender = "Male";

        var actual = await _sut.Get(email, username, gender);

        Assert.NotNull(actual);
        Assert.Equal(email, actual.Email);
        Assert.Equal(username, actual.Username);
        Assert.Equal(gender, actual.Gender);
    }
}