using Book_manager.src.BookManager.Domain.entities;
namespace Book_manager.src.BookManager.Domain.Interfaces;

public interface IUserRepository
{
  Task<User?> GetByEmailAsync(string email);
  Task CreateAsync(User user);
}

