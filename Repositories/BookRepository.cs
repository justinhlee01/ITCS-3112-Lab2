using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Repositories;

public class BookRepository : IBookRepository
{
    private List<Book> _books = new List<Book>();
    private static int _nextIsbn = 1001;

    public void LoadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');

            if (parts.Length < 3)
                continue;

            string author = parts[0].Trim();
            string title = parts[1].Trim();
            string year = parts[2].Trim();

            Book book = new Book(_nextIsbn++, author, title, year, Enums.Genre.Fiction);
            _books.Add(book);
        }
    }

    public Book Addbook(Book book)
    {
        _books.Add(book);
        return book;
    }

    public void Removebook(Book book)
    {
        _books.Remove(book);
    }

    public Book Updatebook(Book book)
    {
        var existing = _books.FirstOrDefault(b => b.Isbn == book.Isbn);
        if (existing != null)
        {
            existing.Author = book.Author;
            existing.Title = book.Title;
            existing.Year = book.Year;
        }
        return book;
    }

    public Book GetBookById(int isbn)
    {
        return _books.FirstOrDefault(b => b.Isbn == isbn);
    }

    public List<Book> GetBookAllBooks()
    {
        return _books;
    }
}