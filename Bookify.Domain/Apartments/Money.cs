namespace Bookify.Domain.Apartments;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first,Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new ApplicationException("currencies doesn't match");
        }

        return first with { Amount = first.Amount + second.Amount };
    }
}