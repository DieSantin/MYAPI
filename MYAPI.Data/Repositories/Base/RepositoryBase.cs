using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MYAPI.Data.Repositories.Base;

public interface IRepositoryBase<T>
{
    Task<int> Count();
    Task<int> Count(Expression<Func<T, bool>> expression);
    Task<bool> Any(Expression<Func<T, bool>> expression);
}

public abstract class RepositoryBase<T> : IRepositoryBase<T>
where T : class
{
    protected readonly IMapper _mapper;
    protected readonly DataContext _context;

    protected RepositoryBase(
        IMapper mapper,
        DataContext factory)
    {
        _mapper = mapper;
        _context = factory;
    }

    public Task<int> Count() 
        => _context.Set<T>().CountAsync();
    public Task<int> Count(Expression<Func<T, bool>> expression) => _context.Set<T>().CountAsync(expression);
    public Task<bool> Any(Expression<Func<T, bool>> expression) => _context.Set<T>().AnyAsync(expression);
}