using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(
        Guid id,
        DateTime createdAt,
        Guid apartmentId,
        Guid userId,
        DateRange dateRange,
        Money priceForPeriod,
        Money amenitiesUpCharge,
        Money discountFee,
        Money totalPrice,
        BookingStatus bookingStatus) : base(id, createdAt)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        DateRange = dateRange;
        PriceForPeriod = priceForPeriod;
        AmenitiesUpCharge = amenitiesUpCharge;
        DiscountFee = discountFee;
        TotalPrice = totalPrice;
        Status = bookingStatus;
    }

    public Guid ApartmentId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange DateRange { get; private set; }

    public Money PriceForPeriod { get; private set; }

    public Money AmenitiesUpCharge { get; private set; }

    public Money DiscountFee { get; private set; }

    public Money TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime? ConfirmedAt { get; private set; }

    public DateTime? RejectedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public DateTime? CancelledAt { get; private set; }

    public static Result<Booking> Reserve(
        Apartment apartment,
        Guid userId,
        DateTime dateNow,
        DateRange dateRange,
        Money discount,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, dateRange, discount);

        var booking = new Booking(
            Guid.CreateVersion7(),
            dateNow,
            apartment.Id,
            userId,
            dateRange,
            pricingDetails.PriceForPeriod,
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.DiscountFee,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved);

        booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));

        return Result.Success(booking);
    }

    public Result Confirm()
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.InvalidStatusForConfirmation);
        }

        Status = BookingStatus.Confirmed;
        ConfirmedAt = DateTime.Now;

        RaiseDomainEvents(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject()
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.InvalidStatusForRejection);
        }

        if (Status == BookingStatus.Rejected)
        {
            return Result.Failure(BookingErrors.AlreadyRejected);
        }

        Status = BookingStatus.Rejected;
        RejectedAt = DateTime.Now;

        RaiseDomainEvents(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.InvalidStatusForCancellation);
        }

        if (Status == BookingStatus.Cancelled)
        {
            return Result.Failure(BookingErrors.AlreadyCancelled);
        }

        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        if (currentDate > DateRange.Start)
        {
            return Result.Failure(BookingErrors.BookingAlreadyStarted);
        }

        Status = BookingStatus.Cancelled;
        CancelledAt = DateTime.Now;

        RaiseDomainEvents(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.InvalidStatusForCompletion);
        }

        Status = BookingStatus.Completed;
        CompletedAt = DateTime.Now;

        RaiseDomainEvents(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }
}
