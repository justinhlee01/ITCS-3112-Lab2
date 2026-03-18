namespace ITCS_3112_Lab2.Domain;

public class Enums
{
    public enum Genre
    {
        Fiction,
        NonFiction,
        Scientific,
        Comedy,
        Action,
        Romance
    }

    public enum Score
    {
        VeryNegative = -5,
        Negative = -3,
        Neutral = 0,
        SlightlyPositive = 1,
        Positive = 3,
        VeryPositive = 5
    }
}