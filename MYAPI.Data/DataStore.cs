using AutoMapper;
using MYAPI.Data.Repositories;

namespace MYAPI.Data;

public interface IDataStore
{
    void Migrate();

    IUsersRepository Users { get; }
}

public class DataStore : IDataStore
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public IUsersRepository Users { get; set; }
    public DataStore(
        IMapper mapper,
        DataContext context)
    {
        _mapper = mapper;
        _context = context;

        Users = new UsersRepository(_mapper, _context);
    }

    public void Migrate()
        => _context.Migrate();
}