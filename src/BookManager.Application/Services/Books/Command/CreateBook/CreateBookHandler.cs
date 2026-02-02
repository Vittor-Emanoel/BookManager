using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;
using Book_manager.src.BookManager.Application.Common.Responses;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, CommandResponse>
    {
        private readonly IBookRepository _booksRepository;

        public CreateBookHandler(IBookRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<CommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(
                request.Name,
                request.Author,
                request.ImageUrl,
                request.Description);

            if (request.Status == Domain.Enums.BookStatus.Reading)
            {
                book.StartReading();
            }

            if (request.Status == Domain.Enums.BookStatus.Read)
            {
                book.StartReading();
                book.FinishReading(request.Rating);
            }

            var success = await _booksRepository.SaveAsync(book);

            if (!success)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = "Falha ao criar o livro."
                };
            }

            return new CommandResponse
            {
                Success = true,
                Message = "Livro criado com sucesso."
            };
        }
    }

}