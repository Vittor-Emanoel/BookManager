using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Domain.Enums;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Command.CreateBook;

public record CreateBookCommand(
    string Name,
    string Author,
    string? ImageUrl,
    int Rating,
    BookStatus Status,
    string? Description
) : IRequest<CommandResponse>;
