using Book_manager.src.BookManager.Application.Common.Responses;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Auth;

public record RegisterCommand(string Name, string Email, string Password) : IRequest<CommandResponse>;