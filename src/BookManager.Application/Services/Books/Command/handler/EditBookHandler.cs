using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;
using Book_manager.src.BookManager.Application.Common.Responses;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook
{
    public class EditBookHandler : IRequestHandler<EditBookCommand, CommandResponse>
    {
        private readonly IBookRepository _booksRepository;
        public EditBookHandler(IBookRepository booksRepository) => _booksRepository = booksRepository;

        public async Task<CommandResponse> Handle(EditBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetByIdAsync(request.BookId);

            if (book is null)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = "Livro não encontrado!."
                };
            }

            book.Update(
                request.Name,
                request.Author,
                request.ImageUrl,
                request.Description,
                request.Status,
                request.Rating
            );

            var success = await _booksRepository.SaveAsync(book);

            return new CommandResponse
            {
                Success = success,
                Message = success ? "Livro editado com sucesso." : "Falha ao editar o livro."
            };
        }
    }

}