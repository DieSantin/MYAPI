using MYAPI.Tests.Factory;
using System.Net;

namespace MYAPI.Tests.EXAPITests;

public class EXAPIControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public EXAPIControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task EXAPIController_GetUserFromInfo_User_Not_Exists_Return_Status_Code_No_Content()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/EXAPI/get-user-from-info/email=notExistingEmail@email.com/username=NotExistingUsername/gender=gender");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task EXAPIController_GetUserFromInfo_User_Exists_In_Database_Status_Code_Ok()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/EXAPI/get-user-from-info/email=existingUser@email.com/username=existingUsername/gender=Male");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}