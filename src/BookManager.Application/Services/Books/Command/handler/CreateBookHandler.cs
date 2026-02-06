using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Application.Interfaces;
using MediatR;
using Book_manager.src.BookManager.Application.Common.Responses;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, CommandResponse>
    {
        private readonly IBookRepository _booksRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateBookHandler(IBookRepository booksRepository, ICurrentUserService currentUserService)
        {
            _booksRepository = booksRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == Guid.Empty)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = "Usuário não autenticado."
                };
            }

            var book = Book.Create(
                userId,
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