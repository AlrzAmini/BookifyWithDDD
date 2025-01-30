using Bookify.Application.Abstractions.Clock;

namespace Bookify.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime DateNow => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}