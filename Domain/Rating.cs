namespace ITCS_3112_Lab2.Domain;

public class Rating
{
    public string MemberID { get; set; }
    public int Isbn { get; set; }
    public Enums.Score Score { get; set; }
    
    public Rating(string memberID, int isbn, Enums.Score score)
    {
        if (string.IsNullOrEmpty(memberID))
            throw new ArgumentNullException(nameof(memberID));
        if (isbn < 0)
            throw new ArgumentOutOfRangeException(nameof(isbn));
        
        MemberID = memberID;
        Isbn = isbn;
        Score = score;
    }
}