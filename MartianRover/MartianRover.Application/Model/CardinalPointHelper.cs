namespace MartianRoverTests;

public static class CardinalPointHelper
{
    const string DIRECTIONS = "NESO";

    public static string ToRight(string currentDirection)
    {
        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection) + 1;
        if (newDirectionIndex > 3)
        {
            newDirectionIndex = 0;
        }
        return DIRECTIONS[newDirectionIndex].ToString();
    }

    public static string ToLeft(string currentDirection)
    {
        var newDirectionIndex = DIRECTIONS.IndexOf(currentDirection) - 1;
        if (newDirectionIndex < 0)
            newDirectionIndex = 3;
        return DIRECTIONS[newDirectionIndex].ToString();
    }
}
