using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Contracts;

public interface IMemberRepository
{
    void AddMember(Member member);
    void RemoveMember(Member member);
    void UpdateMember(Member member);
    Member GetMemberById(string account);
    List<Member> GetAllMembers();
}