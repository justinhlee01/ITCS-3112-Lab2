using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;
using ITCS_3112_Lab2.Repositories;
using ITCS_3112_Lab2.Services;

namespace ITCS_3112_Lab2;

class Program
{
    static IBookRepository _bookRepo = new BookRepository();
    static IMemberRepository _memberRepo = new MemberRepository();
    static IRatingRepository _ratingRepo = new RatingRepository();
    static AuthService _authService;
    static RecommendationService _recService;

    static void Main(string[] args)
    {
        _authService = new AuthService(_memberRepo);
        _recService = new RecommendationService(_ratingRepo, _bookRepo, _memberRepo);

        Console.WriteLine("Book Recommendation System");
        Console.WriteLine("--------------------------");

        Console.Write("Enter path to books.txt: ");
        string booksPath = Console.ReadLine();

        Console.Write("Enter path to ratings.txt: ");
        string ratingsPath = Console.ReadLine();

        if (!File.Exists(booksPath) || !File.Exists(ratingsPath))
        {
            Console.WriteLine("File not found. Please check your paths.");
            return;
        }

        ((BookRepository)_bookRepo).LoadFromFile(booksPath);
        ((MemberRepository)_memberRepo).LoadFromFile(ratingsPath);
        ((RatingRepository)_ratingRepo).LoadFromMembers(_memberRepo.GetAllMembers(), _bookRepo.GetBookAllBooks());

        Console.WriteLine("Data loaded successfully!\n");

        bool running = true;
        while (running)
        {
            if (_authService.IsLoggedIn)
                running = LoggedInMenu();
            else
                running = LoggedOutMenu();
        }

        Console.WriteLine("Goodbye!");
    }

    static bool LoggedOutMenu()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
        Console.Write("Choice: ");
        string input = Console.ReadLine();

        if (input == "1") Login();
        else if (input == "2") Register();
        else if (input == "3") return false;
        else Console.WriteLine("Invalid option.\n");

        return true;
    }

    static bool LoggedInMenu()
    {
        Console.WriteLine("\nWelcome, " + _authService.CurrentMember.Name);
        Console.WriteLine("1. View my ratings");
        Console.WriteLine("2. Rate a book");
        Console.WriteLine("3. Get recommendations");
        Console.WriteLine("4. Add a book");
        Console.WriteLine("5. Add a member");
        Console.WriteLine("6. Logout");
        Console.WriteLine("7. Exit");
        Console.Write("Choice: ");
        string input = Console.ReadLine();

        if (input == "1") ViewRatings();
        else if (input == "2") RateBook();
        else if (input == "3") GetRecommendations();
        else if (input == "4") AddBook();
        else if (input == "5") AddMember();
        else if (input == "6")
        {
            _authService.Logout();
            Console.WriteLine("Logged out.\n");
        }
        else if (input == "7") return false;
        else Console.WriteLine("Invalid option.\n");

        return true;
    }

    static void Login()
    {
        Console.Write("Account number: ");
        string account = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (_authService.Login(account, password))
            Console.WriteLine("Welcome back, " + _authService.CurrentMember.Name + "!\n");
        else
            Console.WriteLine("Wrong account or password.\n");
    }

    static void Register()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        string account = (_memberRepo.GetAllMembers().Count + 1).ToString();
        _authService.Register(account, name, password);
        Console.WriteLine("Account created! Your account number is " + account + "\n");
    }

    static void ViewRatings()
    {
        var ratings = _ratingRepo.GetRatingsByMember(_authService.CurrentMember.Account);
        var books = _bookRepo.GetBookAllBooks();

        Console.WriteLine("\nYour ratings:");

        bool hasRatings = false;
        foreach (var r in ratings)
        {
            if (r.Score == Enums.Score.Zero) continue;
            var book = books.FirstOrDefault(b => b.Isbn == r.Isbn);
            if (book != null)
            {
                Console.WriteLine(book.Title + " by " + book.Author + " - " + r.Score);
                hasRatings = true;
            }
        }

        if (!hasRatings)
            Console.WriteLine("You have not rated any books yet.");

        Console.WriteLine();
    }

    static void RateBook()
    {
        var books = _bookRepo.GetBookAllBooks();

        Console.WriteLine("\nBooks:");
        for (int i = 0; i < books.Count; i++)
            Console.WriteLine((i + 1) + ". " + books[i].Title + " by " + books[i].Author);

        Console.Write("Pick a book: ");
        int choice = int.Parse(Console.ReadLine());
        Book book = books[choice - 1];

        Console.WriteLine("1. -5  Hated it");
        Console.WriteLine("2. -3  Didn't like it");
        Console.WriteLine("3.  0  Haven't read it");
        Console.WriteLine("4.  1  Ok");
        Console.WriteLine("5.  3  Liked it");
        Console.WriteLine("6.  5  Really liked it");
        Console.Write("Your rating: ");
        string ratingInput = Console.ReadLine();

        Enums.Score score = Enums.Score.Zero;
        if (ratingInput == "1") score = Enums.Score.NegFive;
        else if (ratingInput == "2") score = Enums.Score.NegThree;
        else if (ratingInput == "3") score = Enums.Score.Zero;
        else if (ratingInput == "4") score = Enums.Score.One;
        else if (ratingInput == "5") score = Enums.Score.Three;
        else if (ratingInput == "6") score = Enums.Score.Five;

        _ratingRepo.AddRating(new Rating(_authService.CurrentMember.Account, book.Isbn, score));
        Console.WriteLine("Rating saved!\n");
    }

    static void GetRecommendations()
    {
        var recommendations = _recService.GetRecommendations(_authService.CurrentMember.Account);

        Console.WriteLine("\nRecommended books:");

        if (recommendations.Count == 0)
        {
            Console.WriteLine("No recommendations found.\n");
            return;
        }

        foreach (var book in recommendations)
            Console.WriteLine(book.Title + " by " + book.Author + " (" + book.Year + ")");

        Console.WriteLine();
    }

    static void AddBook()
    {
        Console.Write("Author: ");
        string author = Console.ReadLine();
        Console.Write("Title: ");
        string title = Console.ReadLine();
        Console.Write("Year: ");
        string year = Console.ReadLine();

        int isbn = _bookRepo.GetBookAllBooks().Max(b => b.Isbn) + 1;
        _bookRepo.Addbook(new Book(isbn, author, title, year, Enums.Genre.Fiction));
        Console.WriteLine("Book added!\n");
    }

    static void AddMember()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        string account = (_memberRepo.GetAllMembers().Count + 1).ToString();
        _memberRepo.AddMember(new Member(account, name, password, Array.Empty<int>()));
        Console.WriteLine("Member added! Account number: " + account + "\n");
    }
}