using Microsoft.Extensions.DependencyInjection;
using MYAPI.ExApi;
using MYAPI.ExApi.Repositories;
using MYAPI.Tests.Integration;

namespace MYAPI.Integration.Tests.ExApi.Repositories;

public class UserServiceTests : IntegrationBase
{
    private readonly IUsersRepository _sut;

    public UserServiceTests(SystemDescriptor factory)
        : base(factory)
    {
        _sut = factory.Provider.GetService<IExApiWrapper>().Users;
    }

    [Fact]
    public async Task List_random_should_get_random_users()
    {
        var count = 10;

        var actual = await _sut.ListRandom(10);

        Assert.Equal(count, actual.Count);
    }
}