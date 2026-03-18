using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;
using ITCS_3112_Lab2.Repositories;

namespace ITCS_3112_Lab2.Services;

public class AuthService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IRatingRepository _ratingRepository;
    bool isLoggedIn = false;
    public AuthService(IBookRepository bookRepository, IMemberRepository memberRepository, IRatingRepository ratingRepository)
    {
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
        _ratingRepository = ratingRepository;
    }

    public void login(string email, string password)
    {
        var member = _memberRepository.GetMemberById(email);
        if (member == null) throw new ArgumentException("Email not found");
        if (member.Password != password) throw new ArgumentException("Passwords do not match");
        isLoggedIn = true;
    }

    public void logout()
    {
        isLoggedIn = false;
    }
}