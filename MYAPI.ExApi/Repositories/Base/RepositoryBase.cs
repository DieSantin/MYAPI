using AutoMapper;

namespace MYAPI.ExApi.Repositories.Base;

public interface IRepositoryBase<T>
{
    Task<TResult> Get<TResult>(string url);
}

public abstract class RepositoryBase<T> : IRepositoryBase<T>
where T : class
{
    protected readonly IMapper _mapper;
    protected readonly ExApiContext _context;

    protected RepositoryBase(
        IMapper mapper,
        ExApiContext factory)
    {
        _mapper = mapper;
        _context = factory;
    }

    public Task<TResult> Get<TResult>(string url)
        => _context.Get<TResult>(url);
}