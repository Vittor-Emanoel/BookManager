namespace Book_manager.src.BookManager.Application.Interfaces;

public interface IJwtService
{
  string GenerateToken(Guid Id, string name, string email);
}