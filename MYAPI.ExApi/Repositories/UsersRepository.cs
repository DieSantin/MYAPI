using AutoMapper;
using MYAPI.ExApi.Repositories.Base;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.ExApi.Models;

namespace MYAPI.ExApi.Repositories;

public interface IUsersRepository : IRepositoryBase<User>
{
    Task<List<UserDto>> ListRandom(int count);
}

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(
        IMapper mapper,
        ExApiContext context)
        : base(mapper, context)
    {
    }

    public async Task<List<UserDto>> ListRandom(int count)
    {
        var users = await Get<List<User>>($"users/random_user?size={count}");

        foreach (var u in users ?? new List<User>())
        {
            u.CreationDate = DateTime.Now;
            if (u.Address != null)
                u.Address.CreationDate = DateTime.Now;
        }

        return _mapper.Map<List<UserDto>>(users);
    }
}