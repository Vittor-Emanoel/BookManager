
using Book_manager.Domain.Entities;

namespace Book_manager.Repository
{

  public interface IBookSearchQuery
  {
    public string? Name { get; init; }
    public string? Author { get; init; }
    public int? MinRating { get; init; }
    public BookStatus? Status { get; init; }
    public int PageSize { get; set; }
    public int PageNumber { get; init; }
  }

  public interface IBooksRepository
  {
    Task<IEnumerable<Book>> GetAll();
    Task<Book?> GetByIdAsync(int id);
    Task<bool> SaveAsync(Book item);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query);
  }
}