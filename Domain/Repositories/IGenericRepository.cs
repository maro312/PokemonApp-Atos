namespace Domain.Repositories;

public interface IGenericRepository<TEntity, TId> where TEntity : class
{
    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns> returns true if success otherwise false</returns>
    Task<bool> AddAsync(TEntity entity);
    /// <summary>
    /// Edit entity
    /// </summary>
    /// <param name="entity"> </param>
    /// <returns> returns true if success otherwise false</returns>
    Task<bool> UpdateAsync(TEntity entity);
    /// <summary>
    /// Get all
    /// </summary>
    /// <returns> returns list of entities</returns>
    IQueryable<TEntity> GetAllQuerable();
    Task<ICollection<TEntity>> GetAllAsync();
    Task<IList<TEntity>> GetAllPagenatedAsync(int pageSize, int pageNumber);
    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns> returns true if success otherwise false </returns>
    Task<bool> DeleteAsync(TEntity entity);

    /// <summary>
    /// Get entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns> returns specific entity </returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns> returns true if exists otherwise false </returns>
    Task<bool> ExistsAsync(TId id);

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync();
}
