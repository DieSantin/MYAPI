using MYAPI.Models.EXAPIDTOs;

namespace MYAPI.Services.EXAPI;

public interface IEXAPIService
{
    Task<UserAndAddressDTO> GetUserFromEXAPI(string email, string username, string gender);
    Task<UserAndAddressDTO> GetUserFromInfo(string email, string username, string gender);
}
