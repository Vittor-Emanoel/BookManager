using Book_manager.Domain.Commands.Responses;
using Book_manager.Domain.Entities;
using MediatR;

namespace Book_manager.Domain.Commands.Requests
{
  public class CreateBookRequestCommand : IRequest<CreateBookResponse>
  {
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int Rating { get; set; }
    public BookStatus Status { get; set; }
    public string? Description { get; set; }
  }
}