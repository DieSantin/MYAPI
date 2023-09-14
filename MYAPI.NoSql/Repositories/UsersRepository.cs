using AutoMapper;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.NoSql.Entities;
using MYAPI.NoSql.Repositories.Base;

namespace MYAPI.NoSql.Repositories;

public interface IUsersRepository : IRepositoryBase<User>
{
    UserDto Get(string email, string username, string gender);
}

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(
        IMapper mapper,
        NoSqlContext context)
        : base(mapper, context)
    {
    }

    public UserDto Get(string email, string username, string gender)
    {
        var user = _context.Users
            .Where(n => 
                n.Username == username && 
                n.Email == email && 
                n.Gender == gender)
            .FirstOrDefault();

        return _mapper.Map<UserDto>(user);
    }
}