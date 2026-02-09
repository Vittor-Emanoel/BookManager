using Book_manager.src.BookManager.Api.DTO;
using Book_manager.src.BookManager.Application.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_manager.src.BookManager.Api.controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;

  public AuthController(IMediator mediator) => _mediator = mediator;

  [HttpPost]
  [HttpPost("register")]
  public async Task<ActionResult> Register(RegisterUserRequest request)
  {
    var command = new RegisterCommand(
      request.Name,
      request.Email,
      request.Password
    );

    var result = await _mediator.Send(command);

    if (result.UserId == Guid.Empty)
      return BadRequest(result);

    return Ok(result);
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login(LoginUserRequest request)
  {
    var command = new LoginCommand(request.Email, request.Password);

    var result = await _mediator.Send(command);

    if (string.IsNullOrEmpty(result.AccessToken))
      return BadRequest(result);

    return Ok(result);
  }

}