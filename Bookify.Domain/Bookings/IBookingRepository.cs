using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public interface IBookingRepository
{
    Task<Guid> Add(Booking booking, CancellationToken cancellationToken);

    Task<bool> IsOverlapping(Apartment apartment, DateRange dateRange, CancellationToken cancellationToken);
}