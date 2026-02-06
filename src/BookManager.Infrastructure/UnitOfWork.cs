using System.Data;
using Book_manager.src.BookManager.Domain.Interfaces;
using Npgsql;

namespace Book_manager.src.BookManager.Infrastructure;

/// <summary>
/// Implementação de Unit of Work para gerenciar transações atômicas
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _dbConnection;
    private IDbTransaction? _transaction;

    public UnitOfWork(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task BeginTransactionAsync()
    {
        if (_dbConnection.State != ConnectionState.Open)
        {
            _dbConnection.Open();
        }

        _transaction = _dbConnection.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        try
        {
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        await Task.CompletedTask;
    }

    public async Task RollbackAsync()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _transaction = null;

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
