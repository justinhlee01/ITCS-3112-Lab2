namespace ITCS_3112_Lab2.Domain;

public class Member
{
    public string Account { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int[] Ratings { get; set; }

    public Member(string account, string name, string password, int[] ratings)
    {
        if (string.IsNullOrEmpty(account))
            throw new ArgumentNullException(nameof(account));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        Account = account;
        Name = name;
        Password = password;
        Ratings = ratings ?? Array.Empty<int>();
    }
}