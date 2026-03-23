using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Services;

public class AuthService
{
    private readonly IMemberRepository _memberRepository;
    public Member? CurrentMember { get; private set; }
    public bool IsLoggedIn => CurrentMember != null;

    public AuthService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public bool Login(string account, string password)
    {
        var member = _memberRepository.GetMemberById(account);
        if (member == null || member.Password != password) return false;
        CurrentMember = member;
        return true;
    }
 
    public void Logout() => CurrentMember = null;
 
    public Member Register(string account, string name, string password)
    {
        var member = new Member(account, name, password, Array.Empty<int>());
        _memberRepository.AddMember(member);
        return member;
    }
}