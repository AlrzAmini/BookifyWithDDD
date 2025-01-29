﻿namespace Bookify.Domain.Shared;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new ApplicationException("currencies doesn't match");
        }

        return first with { Amount = first.Amount + second.Amount };
    }

    public static Money Zero() => new(0, Currency.None);

    public static Money Zero(Currency currency) => new(0, currency);

    public bool IsZero() => this == Zero();

    public bool IsZero(Currency currency) => this == Zero(currency);
}