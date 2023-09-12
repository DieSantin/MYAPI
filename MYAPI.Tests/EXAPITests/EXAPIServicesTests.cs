using System.Net;
using MYAPI.Services.EXAPI;
using Moq;
using MYAPI.Data.Context;
using Microsoft.EntityFrameworkCore;
using MYAPI.Models.EXAPIEntities;
using AutoMapper;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Tests.MockServices;

namespace MYAPI.Tests.EXAPITests;

public class EXAPIServicesTests
{
    private IEXAPIService _exapiNotFound;
    private IEXAPIService _exapi;

    public EXAPIServicesTests()
    {
        var stub = FakeUsersForUnitTests.GenerateUsers(10);
        var data = stub.AsQueryable();

        var mockFactory = new Mock<IHttpClientFactory>();
        var mockFactoryNotFound = new Mock<IHttpClientFactory>();
        var mockAppDbContext = new Mock<AppDbContext>();
        var mockImapper = new Mock<IMapper>();

        var mockset = new Mock<DbSet<User>>();
        mockset.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        mockset.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockset.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        mockset.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        mockAppDbContext.Setup(x => x.Users).Returns(mockset.Object);

        var modifiedUser = mockset.Object.First();
        modifiedUser.Username = "existingUsername";
        modifiedUser.Email = "existingEmail@email.com";
        modifiedUser.Gender = "existingGender";

        var mockHttpClientFactory = new MockHttpClientFactory(HttpStatusCode.OK);
        mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClientFactory.Client);

        var mockHttpClientFactoryNotFound = new MockHttpClientFactory(HttpStatusCode.NotFound);
        mockFactoryNotFound.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClientFactoryNotFound.Client);

        _exapi = new EXAPIService(mockAppDbContext.Object, mockFactory.Object, mockImapper.Object);
        _exapiNotFound = new EXAPIService(mockAppDbContext.Object, mockFactoryNotFound.Object, mockImapper.Object);
    }

    [Fact]
    public async Task GetUserFromInfo_User_Does_Not_Exist_Return_Null()
    {
        var response = await _exapi.GetUserFromInfo("notExistingEmail@email.com", "notExistingrandom.username", "Gender");
        Assert.Null(response);
    }

    [Fact]
    public async Task GetUserFromInfo_User_Does_Not_Exist_In_Local_Database_Exists_In_EXAPI_Return_UserAndAddressDTO()
    {
        var response = await _exapi.GetUserFromInfo("blair.vandervort@email.com", "blair.vandervort", "Genderfluid");
        Assert.IsType<UserAndAddressDTO>(response);
    }

    [Fact]
    public async Task GetUserFromInfo_UserExistInLocalDatabase_Return_UserAndAddressDTO()
    {
        var response = await _exapi.GetUserFromInfo("existingEmail@email.com", "existingUsername", "existingGender");
        Assert.IsType<UserAndAddressDTO>(response);
    }

    [Fact]
    public async Task GetUserFromEXAPI_User_Does_Not_Exist_In_EXAPI_Return_Null()
    {
        var response = await _exapi.GetUserFromEXAPI("notExistingEmail@email.com", "notExistingrandom.username", "Gender");
        Assert.Null(response);
    }

    [Fact]
    public async Task GetUserFromEXAPI_User_Exist_In_EXAPI_Return_UserAndAddressDTO()
    {
        var response = await _exapi.GetUserFromEXAPI("blair.vandervort@email.com", "blair.vandervort", "Genderfluid");
        Assert.IsType<UserAndAddressDTO>(response);
    }

    [Fact]
    public async Task GetUserFromEXAPI_EXAPI_Not_Found_Return_404()
    {
        var response = await _exapiNotFound.GetUserFromEXAPI("randomEmail@email.com", "random.username", "randomGender");
        Assert.Null(response);
        Assert.True(EXAPIService.EXAPINotFound);
    }
}
