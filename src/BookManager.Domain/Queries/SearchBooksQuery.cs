using Book_manager.src.BookManager.Domain.Enums;
using Book_manager.src.BookManager.Domain.Interfaces;

namespace Book_manager.src.BookManager.Domain.Queries;

public class BookSearchQuery : IBookSearchQuery
{
  public Guid? UserId { get; init; }
  public string? Name { get; init; }
  public string? Author { get; init; }
  public int? MinRating { get; init; }
  public BookStatus? Status { get; init; }
  public int PageSize { get; init; }
  public int PageNumber { get; init; }
}

