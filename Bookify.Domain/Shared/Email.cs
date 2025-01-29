namespace Bookify.Domain.Shared;

using Bookify.Domain.Abstractions;
using System.Text.RegularExpressions;

public record Email
{
    private Email()
    {

    }

    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public required string Value { get; init; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(EmailErrors.EmptyEmail);
        }

        return !EmailRegex.IsMatch(email) ? Result.Failure<Email>(EmailErrors.InvalidEmailFormat) : Result.Success(new Email
        {
            Value = email
        });
    }

    public override string ToString() => Value;
}

