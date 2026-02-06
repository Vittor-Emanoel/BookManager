using Book_manager.src.BookManager.Domain.Interfaces;
using Book_manager.src.BookManager.Application.Interfaces;
using MediatR;
using Book_manager.src.BookManager.Application.Common.Responses;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook
{
    public class EditBookHandler : IRequestHandler<EditBookCommand, CommandResponse>
    {
        private readonly IBookRepository _booksRepository;
        private readonly ICurrentUserService _currentUserService;

        public EditBookHandler(IBookRepository booksRepository, ICurrentUserService currentUserService)
        {
            _booksRepository = booksRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CommandResponse> Handle(EditBookCommand request, CancellationToken cancellationToken)
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

            // Verifica se o livro pertence ao usuário
            var book = await _booksRepository.GetByIdAndUserIdAsync(request.BookId, userId);

            if (book is null)
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = "Livro não encontrado ou você não tem permissão para editá-lo."
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

            // Usa UpdateAsync ao invés de SaveAsync
            var success = await _booksRepository.UpdateAsync(book);

            return new CommandResponse
            {
                Success = success,
                Message = success ? "Livro editado com sucesso." : "Falha ao editar o livro."
            };
        }
    }

}