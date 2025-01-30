namespace Bookify.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    public DateTime DateNow { get; }
    public DateTime UtcNow { get; }
}