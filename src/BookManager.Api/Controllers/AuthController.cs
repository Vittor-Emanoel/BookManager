using Book_manager.src.BookManager.Api.DTO;
using Book_manager.src.BookManager.Application.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_manager.src.BookManager.Api.controllers;

[Route("Api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IMediator _mediator;

  public AuthController(IMediator mediator) => _mediator = mediator;

  [HttpPost]
  public async Task<ActionResult> Register(RegisterUserRequest request)
  {
    var command = new RegisterCommand(
      request.Name,
      request.Email,
      request.Password
    );

    var result = await _mediator.Send(command);

    if (!result.Success)
      return BadRequest(result);

    return Ok(result);
  }

  [HttpPost("Login")]
  public async Task<ActionResult> Login(LoginUserRequest request)
  {
    var command = new LoginCommand(request.Email, request.Password);

    var result = await _mediator.Send(command);

    if (!result.Success)
      return BadRequest(result);

    return Ok(result);
  }

}