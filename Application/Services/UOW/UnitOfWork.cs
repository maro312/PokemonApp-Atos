using Application.Contracts.UOW;
using infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Services.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly PokemonDbContext _dbContext;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(PokemonDbContext dbContext, 
        IDbContextTransaction currentTransaction)
    {
        _dbContext = dbContext;
        _currentTransaction = currentTransaction;
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) 
        {
            return; // A transaction is already in progress, so we don't need to start a new one.
        }

        _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();

            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _dbContext.Dispose();
    }
}
