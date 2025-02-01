using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Shared;
using Bookify.Domain.Users;

namespace Bookify.Application.Features.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler(
    IBookingRepository bookingRepository,
    IApartmentRepository apartmentRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider,
    PricingService pricingService)
    : ICommandHandler<ReserveBookingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartment = await apartmentRepository.GetById(request.UserId, cancellationToken);
        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var dateRange = DateRange.Create(request.StartDate, request.EndDate);

        var isOverlapping = await bookingRepository.IsOverlapping(apartment, dateRange, cancellationToken);
        if (isOverlapping)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        try
        {
            var bookingResult = Booking.Reserve(apartment,
                user.Id,
                dateTimeProvider.DateNow,
                dateRange,
                Money.Zero(),
                pricingService);
            if (bookingResult.IsFailure)
            {
                return Result.Failure<Guid>(bookingResult.Error);
            }

            var bookingId = await bookingRepository.Add(bookingResult.Value, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(bookingId);
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
    }
}