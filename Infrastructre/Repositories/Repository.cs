using Core.Contracts;
using Domain.Repositories;
using infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace infrastructure.Repositories;

public class Repository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    private readonly PokemonDbContext _context;

    public Repository(PokemonDbContext context)
    {
        _context = context;
    }

    ///<inheritdoc/>
    public async Task<bool> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    ///<inheritdoc/>
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    ///<inheritdoc/>
    public IQueryable<TEntity> GetAllQuerable()
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    ///<inheritdoc/>
    public virtual async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    ///<inheritdoc/>
    public async Task<IList<TEntity>> GetAllPagenatedAsync(int pageSize, int pageNumber)
    {
        return await _context.Set<TEntity>().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
    }

    ///<inheritdoc/>
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    ///<inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    ///<inheritdoc/>
    public Task<bool> ExistsAsync(TKey id)
    {
        return _context.Set<TEntity>().AnyAsync(e => e.Id.Equals(id));
    }

    ///<inheritdoc/>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
