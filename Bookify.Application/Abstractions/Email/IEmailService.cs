namespace Bookify.Application.Abstractions.Email;

public interface IEmailService
{
    Task Send(EmailDto email, CancellationToken cancellationToken);
}