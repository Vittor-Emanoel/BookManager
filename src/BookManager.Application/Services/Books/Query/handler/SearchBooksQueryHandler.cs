using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Domain.Queries;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, CommandResponse>
{
    private readonly IBookRepository _bookRepository;
    public SearchBooksQueryHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;
    public async Task<CommandResponse> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
    {
        IBookSearchQuery filter = BuildSearchFilter(request);

        var books = await _bookRepository.SearchAsync(filter);

        return new CommandResponse
        {
            Data = books,
            Success = true
        };
    }

    private static IBookSearchQuery BuildSearchFilter(SearchBooksQuery request)
    {
        return new BookSearchQuery
        {
            Name = request.Query,
            Status = request.BookStatus,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }

}