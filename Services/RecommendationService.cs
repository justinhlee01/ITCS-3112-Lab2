using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Services;

public class RecommendationService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;

    public RecommendationService(IRatingRepository ratingRepository, IBookRepository bookRepository,
        IMemberRepository memberRepository)
    {
        _ratingRepository = ratingRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public List<Book> GetRecommendations(string email)
    {
        var allRatings = _ratingRepository.GetAllRatings();
        var targetRatings = allRatings.Where(r => r.MemberID == email).ToList();
        var ratedIsbns = targetRatings.Select(r => r.Isbn).ToHashSet();
        var allMembers = _memberRepository.GetMemberAllMembers();
        Member? bestMatch = null;
        int bestScore = int.MinValue;

        foreach (var member in allMembers)
        {
            if (member.Email == email) continue;

            var otherRatings = allRatings.Where(r => r.MemberID == member.Email).ToList();
            int dot = ComputeDotProduct(targetRatings, otherRatings);

            if (dot > bestScore)
            {
                bestScore = dot;
                bestMatch = member;
            }
        }

        if (bestMatch == null) return new List<Book>();

        return allRatings
            .Where(r => r.MemberID == bestMatch.Email && !ratedIsbns.Contains(r.Isbn))
            .Select(r => _bookRepository.GetBookById(r.Isbn))
            .Where(b => b != null)
            .ToList()!;
    }

    private int ComputeDotProduct(List<Rating> a, List<Rating> b)
    {
        return a.Join(b,
            ra => ra.Isbn,
            rb => rb.Isbn,
            (ra, rb) => ScoreToInt(ra.Score) * ScoreToInt(rb.Score))
            .Sum();
    }

    private int ScoreToInt(Enums.Score score) => score switch
    {
        Enums.Score.NegFive  => -5,
        Enums.Score.NegThree => -3,
        Enums.Score.Zero     => 0,
        Enums.Score.One      => 1,
        Enums.Score.Three    => 3,
        Enums.Score.Five     => 5,
        _ => 0
    };
}