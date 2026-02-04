using System.ComponentModel.DataAnnotations;
using Book_manager.src.BookManager.Application.Common.Responses;
using MediatR;

namespace Book_manager.src.BookManager.Application.Services.Auth;

public record LoginCommand(
  [EmailAddress]
  string Email,
  [MaxLength(8)]
  string Password) : IRequest<CommandResponse>;