using Book_manager.Domain.Commands.Requests;
using Book_manager.Domain.Commands.Responses;
using Book_manager.Domain.Entities;
using Book_manager.Repository;
using MediatR;

namespace Book_manager.Domain.handlers
{
  public class CreateBookHandler : IRequestHandler<CreateBookRequestCommand, CreateBookResponse>
  {
    private readonly IBooksRepository _booksRepository;

    public CreateBookHandler(IBooksRepository booksRepository)
    {
      _booksRepository = booksRepository;
    }

    public async Task<CreateBookResponse> Handle(CreateBookRequestCommand request, CancellationToken cancellationToken)
    {
      var bookToSave = new Book
      {
        Name = request.Name,
        Author = request.Author,
        ImageUrl = request.ImageUrl,
        Rating = request.Rating,
        Status = request.Status,
        Description = request.Description
      };

      var success = await _booksRepository.SaveAsync(bookToSave);

      if (!success)
      {
        return new CreateBookResponse { Success = false, Message = "Falha ao criar o livro." };
      }

      return new CreateBookResponse
      {
        Success = true,
        Message = "Livro criado com sucesso."
      };
    }
  }

}