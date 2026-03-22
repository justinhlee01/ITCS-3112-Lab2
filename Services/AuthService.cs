using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;
using ITCS_3112_Lab2.Repositories;

namespace ITCS_3112_Lab2.Services;

public class AuthService
{
    private readonly IMemberRepository _memberRepository;
    public Member? CurrentMember { get; private set; }
    public bool isLoggedIn => CurrentMember != null;
    public AuthService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public bool login(string email, string password)
    {
        var member = _memberRepository.GetMemberById(email);
        if (member == null || member.Password != password) return false;
        CurrentMember = member;
        return true;
    }

    public void logout() => CurrentMember = null;

    public Member Register(string email, string name, string password)
    {
        var member = new Member(email, name, password);
        _memberRepository.AddMember(member);
        return member;
    }
}