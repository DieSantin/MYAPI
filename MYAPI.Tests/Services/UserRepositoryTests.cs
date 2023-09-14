using MYAPI.Models.EXAPIDTOs;
using MYAPI.Services;
using MYAPI.Tests.Mocks;

namespace MYAPI.Tests.Services;

public class UserRepositoryTests
{
    private MockData _data;
    private MockExApi _exApi;

    private IUserService _sut;

    public UserRepositoryTests()
    {
        _data = new MockData();
        _exApi = new MockExApi();

        _sut = new UserService(_data.Store.Object, _exApi.Wrapper.Object);
    }

    [Fact]
    public async Task Get_shoul_return_if_user_from_db()
    {
        var email = "asd@asd.com";
        var username = "username";
        var gender = "Male";

        var user = new UserDto
        {
            Email = email,
            Username = username,
            Gender = gender
        };

        _data.Users.Setup(u => u.Get(email, username, gender, false, false, false))
            .Returns(Task.FromResult(user));

        var response = await _sut.Get(email, username, gender);

        Assert.Equal(user, response);
    }
}
