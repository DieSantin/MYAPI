using Microsoft.Extensions.DependencyInjection;
using MYAPI.Data;
using MYAPI.Data.Repositories;
using MYAPI.Models.EXAPIDTOs;

namespace MYAPI.Tests.Integration.Data.Repositories
{
    public class UserRepositoryTests : IntegrationBase
    {
        private readonly IUsersRepository _sut;

        public UserRepositoryTests(SystemDescriptor factory)
            : base(factory)
        {
            _sut = factory.Provider.GetService<IDataStore>().Users;
        }

        [Fact]
        public async Task Test_flow()
        {
            var email = "asd@asd.com";
            var username = "username";
            var gender = "Male";

            var users = new List<UserDto>
            {
                new UserDto
                {
                    Email = email,
                    Gender = gender,
                    Username = username
                }
            };

            await _sut.CreateAll(users);

            var user = await _sut.Get(email, username, gender);

            Assert.NotNull(user);
        }
    }
}
