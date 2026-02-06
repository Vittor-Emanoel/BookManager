namespace Book_manager.src.BookManager.Domain.Interfaces;

/// <summary>
/// Interface para gerenciar operações atômicas (transações)
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
