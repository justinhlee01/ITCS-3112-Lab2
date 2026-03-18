using ITCS_3112_Lab2.Domain;

namespace ITCS_3112_Lab2.Contracts;

public interface IRatingRepository
{ 
    void AddRating(Rating  rating);
    void UpdateRating(Rating rating);
    List<Rating> GetAllRatings();
    List<Rating> GetMemberById(Member member);
}