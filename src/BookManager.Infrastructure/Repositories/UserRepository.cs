using System.Data;
using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using Dapper;

namespace Book_manager.src.BookManager.Infrastructure.repositories;

public class UserRepository : IUserRepository
{
  private readonly IDbConnection DbConnection;

  public UserRepository(IDbConnection dbConnection) => DbConnection = dbConnection;

  public async Task CreateAsync(User user)
  {
    const string query = @"INSERT INTO Users(Id, Name, Email, PasswordHash, CreatedAt)
                          VALUES (@Id, @Name, @Email, @PasswordHash, @CreatedAt)";

    await DbConnection.ExecuteAsync(query, user);
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    const string query = @"SELECT * FROM Users WHERE Email = @Email";

    return await DbConnection.QueryFirstOrDefaultAsync(query, new { Email = email });
  }
}