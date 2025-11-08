namespace Book_manager.Domain.Commands.Responses
{
  public class CreateBookResponse
  {
    public int Id { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
  }
}