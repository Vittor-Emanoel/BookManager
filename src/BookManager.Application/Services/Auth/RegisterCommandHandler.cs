using Book_manager.src.BookManager.Application.Services.Auth;
using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Infrastructure.Security;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
  public readonly IUserRepository _userRepository;

  public RegisterCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

  public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var userAlreadyExists = await _userRepository.GetByEmailAsync(request.Email);

    if (userAlreadyExists is not null)
      throw new Exception("User Already exists");

    /// Inverter essa DEP
    var passwordHash = new PasswordHasher().Hash(request.Password);

    Console.WriteLine(passwordHash);

    var user = User.Create(request.Name, request.Email, passwordHash);

    return user.Id;
  }
}