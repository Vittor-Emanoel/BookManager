using Book_manager.Domain.Commands.Requests;
using Book_manager.Domain.Commands.Responses;

namespace Book_manager.Domain.handlers.interfaces
{
  public interface ICreateBookHandler
  {
    CreateBookResponse Handle(CreateBookRequestCommand command);
  }
}