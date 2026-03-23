using ITCS_3112_Lab2.Contracts;
using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Repositories;

public class RatingRepository : IRatingRepository
{
    private List<Rating> _ratings = new List<Rating>();
    
    public void LoadFromMembers(List<Member> members, List<Book> books)
    {
        foreach (Member member in members)
        {
            for (int i = 0; i < member.Ratings.Length; i++)
            {
                if (i >= books.Count)
                    break;

                int scoreValue = member.Ratings[i];
                int isbn = books[i].Isbn;

                Enums.Score score = scoreValue switch
                {
                    -5 => Enums.Score.NegFive,
                    -3 => Enums.Score.NegThree,
                    1  => Enums.Score.One,
                    3  => Enums.Score.Three,
                    5  => Enums.Score.Five,
                    _  => Enums.Score.Zero
                };

                Rating rating = new Rating(member.Account, isbn, score);
                _ratings.Add(rating);
            }
        }
    }

    public void AddRating(Rating rating)
    {
        var existing = _ratings.FirstOrDefault(r =>
            r.MemberID == rating.MemberID && r.Isbn == rating.Isbn);

        if (existing != null)
            existing.Score = rating.Score;
        else
            _ratings.Add(rating);
    }

    public void UpdateRating(Rating rating)
    {
        var existing = _ratings.FirstOrDefault(r =>
            r.MemberID == rating.MemberID && r.Isbn == rating.Isbn);

        if (existing != null)
            existing.Score = rating.Score;
    }

    public List<Rating> GetAllRatings()
    {
        return _ratings;
    }

    public List<Rating> GetRatingsByMember(string memberId)
    {
        return _ratings.Where(r => r.MemberID == memberId).ToList();
    }
}