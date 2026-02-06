using Book_manager.src.BookManager.Application.Common.Responses;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Command;

public record DeleteBookCommand(int BookId) : IRequest<CommandResponse>;
