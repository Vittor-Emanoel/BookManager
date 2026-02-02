using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Domain.Interfaces;

public interface IBookSearchQuery
{
    public string? Name { get; init; }
    public string? Author { get; init; }
    public int? MinRating { get; init; }
    public BookStatus? Status { get; init; }
    public int PageSize { get; set; }
    public int PageNumber { get; init; }
}

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Book item);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query);
}