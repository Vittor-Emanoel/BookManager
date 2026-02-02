using Book_manager.src.BookManager.Domain.Enums;

namespace Book_manager.src.BookManager.Domain.entities;

public class Book
{
    public long Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public int Rating { get; private set; }
    public BookStatus Status { get; private set; }
    public string? Description { get; private set; }

    private Book() { }

    public static Book Create(
        string name,
        string author,
        string? imageUrl,
        string? description
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("'name' is required.");

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("'author' is required.");


        return new Book
        {
            Name = name,
            Author = author,
            ImageUrl = imageUrl,
            Status = BookStatus.ToRead,
            Description = description
        };


    }

    public void StartReading()
    {
        if (Status != BookStatus.ToRead)
            throw new InvalidOperationException();

        Status = BookStatus.Reading;
    }

    public void FinishReading(int rating)
    {
        if (Status != BookStatus.Reading)
            throw new InvalidOperationException();

        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating));

        Rating = rating;
        Status = BookStatus.Read;
    }

}