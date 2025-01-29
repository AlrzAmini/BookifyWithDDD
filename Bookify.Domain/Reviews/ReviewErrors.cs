using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews;

public static class ReviewErrors
{
    public static Error BookingNotCompleted =
        new Error(nameof(BookingNotCompleted), "Review can only add to completed bookings.");
}