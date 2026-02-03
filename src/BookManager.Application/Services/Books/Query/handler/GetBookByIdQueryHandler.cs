using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, CommandResponse>
{
  private readonly IBookRepository _bookRepository;

  public GetBookByIdQueryHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

  public async Task<CommandResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
  {
    var book = await _bookRepository.GetByIdAsync(request.BookId);

    return new CommandResponse
    {
      Data = book,
      Success = true
    };
  }
}