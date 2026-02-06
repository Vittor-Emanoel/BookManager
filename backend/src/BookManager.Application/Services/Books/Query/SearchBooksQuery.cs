using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Domain.Enums;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public enum BookOrderBy
{
    Name,
    Author,
    Rating,
    Status,
    CreatedAt
}

public record SearchBooksQuery(
    string? Query,
    BookStatus? BookStatus,
    int Page = 1,
    int PageSize = 10,
    BookOrderBy OrderBy = BookOrderBy.Name,
    bool Desc = false
  ) : IRequest<CommandResponse>;