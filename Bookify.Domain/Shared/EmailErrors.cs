using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Shared;

public static class EmailErrors
{
    public static readonly Error EmptyEmail =
        new("EmptyEmail", "Email cannot be empty.");

    public static readonly Error InvalidEmailFormat =
        new("InvalidEmailFormat", "Email format is invalid.");
}