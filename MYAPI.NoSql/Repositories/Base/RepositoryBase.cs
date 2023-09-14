using AutoMapper;

namespace MYAPI.NoSql.Repositories.Base;

public interface IRepositoryBase<T>
{
}

public abstract class RepositoryBase<T> : IRepositoryBase<T>
where T : class
{
    protected readonly IMapper _mapper;
    protected readonly NoSqlContext _context;

    protected RepositoryBase(
        IMapper mapper,
        NoSqlContext factory)
    {
        _mapper = mapper;
        _context = factory;
    }

}