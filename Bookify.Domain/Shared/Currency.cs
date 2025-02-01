namespace Bookify.Domain.Shared;

public record Currency
{
    internal static readonly Currency None = new("");
    private static readonly Currency Rial = new("IRR");
    private static readonly Currency Usd = new("Usd");
    private static readonly Currency Eur = new("Eur");

    private Currency(string code) => Code = code;

    public string Code { get; init; }

    public static IReadOnlyCollection<Currency> All()
    {
        return new List<Currency>
        {
            Rial,
            Usd,
            Eur,
        };
    }

    public static Currency FromCode(string code)
    {
        return All().FirstOrDefault(c => c.Code == code) ?? Rial;
    }

    
}