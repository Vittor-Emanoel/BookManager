using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Domain.Interfaces;

public interface IBookSearchQuery
{
    Guid? UserId { get; init; }
    string? Name { get; init; }
    string? Author { get; init; }
    int? MinRating { get; init; }
    BookStatus? Status { get; init; }

    int PageSize { get; init; }
    int PageNumber { get; init; }
}


public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllByUserIdAsync(Guid userId);
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByIdAndUserIdAsync(int id, Guid userId);
    Task<bool> SaveAsync(Book item);
    Task<bool> UpdateAsync(Book item);
    Task<bool> DeleteAsync(int id, Guid userId);
    Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query);
}