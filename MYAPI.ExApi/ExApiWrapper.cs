using AutoMapper;
using MYAPI.ExApi.Repositories;

namespace MYAPI.ExApi;

public interface IExApiWrapper
{
    IUsersRepository Users { get; }
}

public class ExApiWrapper : IExApiWrapper
{
    private readonly IMapper _mapper;
    private readonly ExApiContext _context;

    public IUsersRepository Users { get; set; }
    public ExApiWrapper(
        IMapper mapper,
        ExApiContext context)
    {
        _mapper = mapper;
        _context = context;

        Users = new UsersRepository(_mapper, _context);
    }
}