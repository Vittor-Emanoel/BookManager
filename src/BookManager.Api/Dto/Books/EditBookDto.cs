using System.ComponentModel.DataAnnotations;
using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Api.DTO;

public record EditBookRequest(
    [Required]
    int BookId,

    [Required]
    [MinLength(2)]
    string Name,

    [Required]
    [MinLength(2)]
    string Author,

    [Url]
    string? ImageUrl,

    [Range(0, 5)]
    int Rating,

    [Required]
    BookStatus Status,

    [MaxLength(500)]
    string? Description
);