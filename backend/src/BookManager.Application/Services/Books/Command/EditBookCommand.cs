using Book_manager.src.BookManager.Application.Common.Responses;
using Book_manager.src.BookManager.Domain.Enums;
using MediatR;

public record EditBookCommand(
    int BookId,
    string Name,
    string Author,
    string? ImageUrl,
    int Rating,
    BookStatus Status,
    string? Description
) : IRequest<CommandResponse>;