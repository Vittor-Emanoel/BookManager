using System.ComponentModel.DataAnnotations;

namespace Book_manager.src.BookManager.Api.DTO;

public record LoginUserRequest(
  [EmailAddress]
  string Email,
  [MaxLength(8)]
  string Password);