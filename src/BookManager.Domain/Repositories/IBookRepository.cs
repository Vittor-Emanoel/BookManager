using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Domain.Interfaces;

public interface IBookSearchQuery
{
    string? Name { get; init; }
    string? Author { get; init; }
    int? MinRating { get; init; }
    BookStatus? Status { get; init; }

    int PageSize { get; init; }
    int PageNumber { get; init; }
}


public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Book item);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query);
}