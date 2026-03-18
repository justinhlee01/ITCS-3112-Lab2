namespace ITCS_3112_Lab2.Domain;

public class Member
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public Member(string email, string name, string password)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(account));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));

        Email = email;
        Name = name;
        Password = password;
    }
}