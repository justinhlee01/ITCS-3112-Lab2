using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Contracts;

public interface IBookRepository
{
    Book Addbook(Book book);
    void Removebook(Book book);
    Book Updatebook(Book book);
    Book GetBookById(int bookId);
    Book GetBookAllBooks(string title);
}