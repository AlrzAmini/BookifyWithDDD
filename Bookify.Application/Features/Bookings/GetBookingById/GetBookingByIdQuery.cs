using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Features.Bookings.GetBookingById;

public record GetBookingByIdQuery(Guid BookingId) : IQuery<BookingResponse>;