using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Contracts;

public interface IMemberRepository
{
    Member AddMember(Member member);
    void RemoveMember(Member member);
    Member UpdateMember(Member member);
    Member GetMemberById(string email);
    Member GetMemberAllMembers(string email);
}