using Book_manager.Domain.Commands.Requests;
using Book_manager.Domain.Commands.Responses;
using Book_manager.Domain.Entities;
using Book_manager.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_manager.Controllers
{
  [Route("Api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly IBooksRepository _repository;
    private readonly IMediator _mediator;

    public BooksController(IBooksRepository repository, IMediator mediator)
    {
      this._repository = repository;
      this._mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateBookResponse>> Create([FromBody] CreateBookRequestCommand command)
    {
      var result = await _mediator.Send(command);

      if (!result.Success) return BadRequest(result);

      return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<CreateBookResponse>> GetAll([FromBody] CreateBookRequestCommand command)
    {
      var result = await _mediator.Send(command);

      if (!result.Success) return BadRequest(result);

      return Ok(result);
    }

  }
}