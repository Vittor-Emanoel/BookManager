namespace Book_manager.src.BookManager.Application.Interfaces;

/// <summary>
/// Interface para obter informações do usuário autenticado
/// </summary>
public interface ICurrentUserService
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
}
