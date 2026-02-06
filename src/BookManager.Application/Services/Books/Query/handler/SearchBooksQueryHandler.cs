using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Domain.Queries;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, CommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICurrentUserService _currentUserService;

    public SearchBooksQueryHandler(IBookRepository bookRepository, ICurrentUserService currentUserService)
    {
        _bookRepository = bookRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CommandResponse> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
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

        IBookSearchQuery filter = BuildSearchFilter(request, userId);

        var books = await _bookRepository.SearchAsync(filter);

        return new CommandResponse
        {
            Data = books,
            Success = true
        };
    }

    private static IBookSearchQuery BuildSearchFilter(SearchBooksQuery request, Guid userId)
    {
        return new BookSearchQuery
        {
            UserId = userId,
            Name = request.Query,
            Status = request.BookStatus,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }

}