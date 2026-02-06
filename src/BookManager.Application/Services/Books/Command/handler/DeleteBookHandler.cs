using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Application.Interfaces;
using Book_manager.src.BookManager.Domain.Interfaces;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.handler;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, CommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteBookHandler(IBookRepository bookRepository, ICurrentUserService currentUserService)
    {
        _bookRepository = bookRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CommandResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
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

        // Verifica se o livro pertence ao usuário antes de deletar
        var book = await _bookRepository.GetByIdAndUserIdAsync(request.BookId, userId);

        if (book is null)
        {
            return new CommandResponse
            {
                Success = false,
                Message = "Livro não encontrado ou você não tem permissão para deletá-lo."
            };
        }

        var success = await _bookRepository.DeleteAsync(request.BookId, userId);

        return new CommandResponse
        {
            Success = success,
            Message = success ? "Livro deletado com sucesso." : "Falha ao deletar o livro."
        };
    }
}
