using AutoMapper;
using MYAPI.NoSql.Repositories;

namespace MYAPI.NoSql;

public interface INoSqlStore
{
    IUsersRepository Users { get; }
}

public class NoSqlStore : INoSqlStore
{
    private readonly IMapper _mapper;
    private readonly NoSqlContext _context;

    public IUsersRepository Users { get; set; }
    public NoSqlStore(
        IMapper mapper,
        NoSqlContext context)
    {
        _mapper = mapper;
        _context = context;

        Users = new UsersRepository(_mapper, _context);
    }
}