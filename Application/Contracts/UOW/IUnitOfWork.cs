namespace Application.Contracts.UOW;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    /// <returns></returns>
    Task BeginTransactionAsync();
    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    /// <returns></returns>
    Task CommitTransactionAsync();
    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    /// <returns></returns>
    Task RollbackTransactionAsync();
}
