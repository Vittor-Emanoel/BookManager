using System.ComponentModel.DataAnnotations;

namespace Book_manager.src.BookManager.Api.DTO;

public record RegisterUserRequest(
    [Required]
    [MinLength(2)]
    string Name,

    [Required]
    [EmailAddress]
    string Email,

    [MinLength(8)]
    string Password
);