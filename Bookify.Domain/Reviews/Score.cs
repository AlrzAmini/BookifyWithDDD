namespace Bookify.Domain.Reviews;

public record Score
{
    private Score()
    {

    }

    public int Value { get; init; }

    public static Score Create(int score)
    {
        if (score is < 0 or > 5)
        {
            throw new ApplicationException("Score must be between 0 and 5.");
        }

        return new Score
        {
            Value = score
        };
    }
}