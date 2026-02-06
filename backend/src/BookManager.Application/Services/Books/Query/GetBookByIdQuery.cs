using Book_manager.src.BookManager.Application.Common.Responses;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Books.Query;

public record GetBookByIdQuery(int BookId) : IRequest<CommandResponse>;