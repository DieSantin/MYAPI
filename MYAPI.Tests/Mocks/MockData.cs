using Moq;
using MYAPI.Data;
using MYAPI.Data.Repositories;

namespace MYAPI.Tests.Mocks;

public class MockData
{
    public Mock<IDataStore> Store { get; private set; }
    public Mock<IUsersRepository> Users { get; }

    public MockData()
    {
        Store = new Mock<IDataStore>();
        Users = new Mock<IUsersRepository>();

        Store.SetupGet(d => d.Users)
            .Returns(Users.Object);
    }
}