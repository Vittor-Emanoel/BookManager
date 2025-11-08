
namespace Book_manager.Domain.Entities
{
  public class Book
  {
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Rating { get; set; }
    public BookStatus Status { get; set; }
    public string? Description { get; set; }
  }
  public enum BookStatus
  {
    ToRead = 1,
    Reading = 2,
    Read = 3
  }
}

