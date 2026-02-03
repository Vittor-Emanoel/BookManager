using System.ComponentModel.DataAnnotations;
using Book_manager.src.BookManager.Application.Services.Books.Query;
using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Api.DTO;


public record SearchBooksRequest(
    string? query,

    BookStatus? bookStatus,

    [Range(1, int.MaxValue)]
    int Page = 1,

    [Range(1, 100)]
    int PageSize = 10,

    BookOrderBy OrderBy = BookOrderBy.Name,
    bool Desc = false
  );