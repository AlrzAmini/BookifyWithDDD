using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Features.Bookings.GetBookingById;

internal sealed class GetBookingByIdQueryHandler : IQueryHandler<GetBookingByIdQuery, BookingResponse>
{
    public async Task<Result<BookingResponse>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}