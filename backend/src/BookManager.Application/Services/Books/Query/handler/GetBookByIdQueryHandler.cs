using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, CommandResponse>
{
  private readonly IBookRepository _bookRepository;
  private readonly ICurrentUserService _currentUserService;

  public GetBookByIdQueryHandler(IBookRepository bookRepository, ICurrentUserService currentUserService)
  {
    _bookRepository = bookRepository;
    _currentUserService = currentUserService;
  }

  public async Task<CommandResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
  {
    var userId = _currentUserService.UserId;

    if (userId == Guid.Empty)
    {
      return new CommandResponse
      {
        Success = false,
        Message = "Usuário não autenticado."
      };
    }

    // Busca livro que pertence ao usuário
    var book = await _bookRepository.GetByIdAndUserIdAsync(request.BookId, userId);

    if (book is null)
    {
      return new CommandResponse
      {
        Success = false,
        Message = "Livro não encontrado."
      };
    }

    return new CommandResponse
    {
      Data = book,
      Success = true
    };
  }
}