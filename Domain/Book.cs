namespace ITCS_3112_Lab2.Domain;

public class Book
{
    public int Isbn { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public Enums.Genre Genre { get; set; }

    public Book(int isbn, string author, string title, string year, Enums.Genre genre)
    {
        if (isbn <= 0)
            throw new ArgumentOutOfRangeException(nameof(isbn), "ISBN must be a positive number.");
        if (string.IsNullOrEmpty(author))
            throw new ArgumentNullException(nameof(author));
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException(nameof(title));
        if (string.IsNullOrEmpty(year))
            throw new ArgumentNullException(nameof(year));

        Isbn = isbn;
        Author = author;
        Title = title;
        Year = year;
        Genre = genre;
    }
}