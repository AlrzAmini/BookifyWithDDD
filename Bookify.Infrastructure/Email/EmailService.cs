using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task Send(EmailDto email, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}