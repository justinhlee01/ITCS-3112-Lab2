using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Repositories;

public class MemberRepository : IMemberRepository
{
    private List<Member> _members = new List<Member>();
    private static int _nextAccount = 1;
    
    public void LoadFromFile(string path)
    {
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length - 1; i += 2)
        {
            string name = lines[i].Trim();
            string ratingsLine = lines[i + 1].Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ratingsLine))
                continue;

            string[] ratingParts = ratingsLine.Split(' ');
            int[] ratings = new int[ratingParts.Length];

            for (int j = 0; j < ratingParts.Length; j++)
            {
                if (int.TryParse(ratingParts[j].Trim(), out int val))
                    ratings[j] = val;
            }

            string account = (_nextAccount++).ToString();
            Member member = new Member(account, name, "password", ratings);
            _members.Add(member);
        }
    }

    public void AddMember(Member member)
    {
        _members.Add(member);
    }

    public void RemoveMember(Member member)
    {
        _members.Remove(member);
    }

    public void UpdateMember(Member member)
    {
        var existing = _members.FirstOrDefault(m => m.Account == member.Account);
        if (existing != null)
        {
            existing.Name = member.Name;
            existing.Password = member.Password;
        }
    }

    public Member GetMemberById(string account)
    {
        return _members.FirstOrDefault(m => m.Account == account);
    }

    public List<Member> GetAllMembers()
    {
        return _members;
    }
}