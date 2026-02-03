using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_manager.src.BookManager.Api.controllers;

[Route("Api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

  private readonly IMediator _mediator;

  // public async Task<ActionResult> Register()
  // {
  //   var command = await _mediator.Send()

  //   return Task.CompletedTask
  // }

  // public async Task<ActionResult> Login()
  // {
  //   var command = await _mediator.Send()
  // }

}