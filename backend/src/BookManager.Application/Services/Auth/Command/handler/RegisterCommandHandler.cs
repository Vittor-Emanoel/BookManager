using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Application.Services.Auth;
using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;

  public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
  }

  public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
  {
    var userAlreadyExists = await _userRepository.GetByEmailAsync(request.Email);

    if (userAlreadyExists is not null)
      throw new Exception("User Already exists");

    var passwordHash = _passwordHasher.Hash(request.Password);

    var user = User.Create(request.Name, request.Email, passwordHash);

    await _userRepository.CreateAsync(user);

    return new RegisterResponse
    {
      UserId = user.Id
    };
  }


}