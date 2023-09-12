using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MYAPI.Data.Context;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Models.EXAPIEntities;
using Newtonsoft.Json;

namespace MYAPI.Services.EXAPI;

public class EXAPIService : IEXAPIService
{
    public static bool EXAPINotFound = false;

    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppDbContext _context;

    public EXAPIService(AppDbContext context, IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }

    public async Task<UserAndAddressDTO> GetUserFromInfo(string email, string username, string gender)
    {
        var user = _context.Users
            .Where(n => n.Username == username && n.Email == email && n.Gender == gender)
            .Include(n => n.Address)
            .Include(n => n.Employment)
            .Include(n => n.CreditCard)
            .FirstOrDefault();

        var addressDTO = _mapper.Map<AddressDTO>(user);
        var userDTO = _mapper.Map<UserDTO>(user);

        if (user != null)
        {
            return new UserAndAddressDTO
            {
                User = userDTO,
                Address = addressDTO
            };
        }
        else
        {
            return await GetUserFromEXAPI(email, username, gender);
        }
    }

    public async Task<UserAndAddressDTO> GetUserFromEXAPI(string email, string username, string gender)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var EXAPIAddress = new Uri("https://random-data-api.com/api/users/random_user?size=10");
        var response = await httpClient.GetAsync(EXAPIAddress);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);

            foreach (var u in users)
            {
                u.CreationDate = DateTime.Now;
                u.Address.CreationDate = DateTime.Now;
            }

            _context.AddRange(users);
            _context.SaveChanges();

            var userDTO = new UserDTO();
            var addressDTO = new AddressDTO();
            bool userExist = false;

            foreach (var u in users)
            {
                if (u.Email == email && u.Username == username && u.Gender == gender)
                {
                    userDTO = _mapper.Map<UserDTO>(u);
                    addressDTO = _mapper.Map<AddressDTO>(u);
                    userExist = true;
                }
            }
            if (userExist)
            {
                return new UserAndAddressDTO
                {
                    User = userDTO,
                    Address = addressDTO
                };
            }
            else
            {
                return null;
            }
        }
        else
        {
            EXAPINotFound = true;
            return null;
        }
    }
}
