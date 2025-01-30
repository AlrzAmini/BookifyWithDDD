namespace Bookify.Application.Abstractions.Email;

public record EmailDto(Domain.Users.Email Recipient, string Subject, string Body);