using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, CommandResponse>
{

  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtService _jwtService;

  public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
  {
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _jwtService = jwtService;
  }

  async Task<CommandResponse> IRequestHandler<LoginCommand, CommandResponse>.Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetByEmailAsync(request.Email);

    if (user is null)
      throw new Exception("User does not exist!");

    var doesThePasswordMatch = _passwordHasher.Verify(request.Password, user.PasswordHash);

    if (!doesThePasswordMatch)
      throw new Exception("Invalid credentials!");

    var token = _jwtService.GenerateToken(user.Id, user.Name, user.Email);


    return new CommandResponse
    {
      Success = true,
      Data = token
    };
  }
}