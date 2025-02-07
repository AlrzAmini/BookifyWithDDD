using Bookify.Application.Abstractions.Database;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Features.Bookings;

internal sealed class BookingRepository(IApplicationDbContext context)
    : Repository<Booking>(context), IBookingRepository
{
    private static readonly IReadOnlyCollection<BookingStatus> ActiveBookingStatuses = [BookingStatus.Reserved, BookingStatus.Confirmed, BookingStatus.Confirmed];

    public async Task<bool> IsOverlapping(Apartment apartment, DateRange dateRange, CancellationToken cancellationToken)
    {
        return await context
            .Bookings
            .Where(b => b.ApartmentId == apartment.Id)
            .Where(b => b.DateRange.Start <= dateRange.End)
            .Where(b => b.DateRange.End >= dateRange.Start)
            .Where(b => ActiveBookingStatuses.Contains(b.Status))
            .AnyAsync(cancellationToken: cancellationToken);
    }
}