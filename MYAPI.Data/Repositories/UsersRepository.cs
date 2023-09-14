using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MYAPI.Data.Repositories.Base;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Data.Entities;

namespace MYAPI.Data.Repositories;

public interface IUsersRepository : IRepositoryBase<User>
{
    Task<UserDto> Get(string email, string username, string gender, bool includeAddress = false, bool includeEmployment = false, bool includeCard = false);
    Task CreateAll(IEnumerable<UserDto> dtos);
}

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
    public UsersRepository(
        IMapper mapper,
        DataContext context)
        : base(mapper, context)
    {
    }

    public async Task<UserDto> Get(string email, string username, string gender, bool includeAddress = false, bool includeEmployment = false, bool includeCard = false)
    {
        var query = _context.Users
            .Where(n => 
                n.Username == username && 
                n.Email == email && 
                n.Gender == gender);

        if (includeAddress)
            query = query.Include(n => n.Address);
        
        if (includeEmployment)
            query = query.Include(n => n.Employment);
        
        if (includeCard)
            query = query.Include(n => n.CreditCard);

        var entity = await query.FirstOrDefaultAsync();

        return _mapper.Map<UserDto>(entity);
    }

    public async Task CreateAll(IEnumerable<UserDto> dtos)
    {
        await _context.Users.AddRangeAsync(_mapper.Map<IEnumerable<User>>(dtos));
        await _context.SaveChangesAsync();
    }
}