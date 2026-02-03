using Book_manager.src.BookManager.Api.DTO;
using Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook;
using Book_manager.src.BookManager.Application.Services.Books.Query;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book_manager.src.BookManager.Api.controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBookRequest request)
        {

            var command = new CreateBookCommand(
                request.Name,
                request.Author,
                request.ImageUrl,
                request.Rating,
                request.Status,
                request.Description
            );

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> Search([FromQuery] SearchBooksRequest request)
        {
            var query = new SearchBooksQuery(
                request.query,
                request.bookStatus,
                request.Page,
                request.PageSize,
                request.OrderBy
            );

            var result = await _mediator.Send(query);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}