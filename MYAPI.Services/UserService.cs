using MYAPI.Data;
using MYAPI.ExApi;
using MYAPI.Models.EXAPIDTOs;

namespace MYAPI.Services;

public interface IUserService
{
    Task<UserDto?> Get(string email, string username, string gender, bool includeAddress = false, bool includeEmployment = false, bool includeCard = false);
}

public class UserService : IUserService
{
    private readonly IDataStore _database;
    private readonly IExApiWrapper _api;

    public UserService(
        IDataStore data,
        IExApiWrapper api)
    {
        _database = data;
        _api = api;
    }

    public async Task<UserDto?> Get(
        string email, 
        string username, 
        string gender, 
        bool includeAddress = false, 
        bool includeEmployment = false, 
        bool includeCard = false)
    {
        var user = await _database.Users.Get(email, username, gender, includeAddress, includeEmployment, includeCard);

        if (user != null)
            return user;

        var apiUsers = await _api.Users.ListRandom(10);

        await _database.Users.CreateAll(apiUsers);

        return apiUsers.FirstOrDefault(user => 
            user.Email == email &&
            user.Username == username &&
            user.Gender == gender);
    }
}
