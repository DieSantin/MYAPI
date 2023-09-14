using Moq;
using MYAPI.ExApi;
using MYAPI.ExApi.Repositories;

namespace MYAPI.Tests.Mocks;

public class MockExApi
{
    public Mock<IExApiWrapper> Wrapper { get; private set; }
    public Mock<IUsersRepository> Users { get; }

    public MockExApi()
    {
        Wrapper = new Mock<IExApiWrapper>();
        Users = new Mock<IUsersRepository>();

        Wrapper.SetupGet(d => d.Users)
            .Returns(Users.Object);
    }
}