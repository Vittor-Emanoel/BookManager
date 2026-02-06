namespace Book_manager.src.BookManager.Domain.entities;

public class User
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }


  private User() { }

  public static User Create(
          string name,
          string email,
          string passwordHash
      )
  {

    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("'name' is required!.");

    if (string.IsNullOrWhiteSpace(email))
      throw new ArgumentException("'email' is required.");

    if (string.IsNullOrWhiteSpace(passwordHash))
      throw new ArgumentException("'passwordHash' is required.");


    return new User
    {
      Id = Guid.NewGuid(),
      Name = name,
      Email = email,
      PasswordHash = passwordHash,
      CreatedAt = DateTime.UtcNow,
    };

  }

}


