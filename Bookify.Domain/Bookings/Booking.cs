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

    public static Booking Reserve(
        Apartment apartment,
        Guid userId,
        DateRange dateRange,
        Money discount,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, dateRange, discount);

        var booking = new Booking(
            Guid.CreateVersion7(),
            DateTime.Now,
            apartment.Id,
            userId,
            dateRange,
            pricingDetails.PriceForPeriod,
            pricingDetails.AmenitiesUpCharge,
            pricingDetails.DiscountFee,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved);

        booking.RaiseDomainEvents(new BookingReservedDomainEvent(booking.Id));

        return booking;
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Reserved)
        {
            throw new InvalidOperationException("only reserved bookings can be confirmed.");
        }

        Status = BookingStatus.Confirmed;
        ConfirmedAt = DateTime.Now;

        RaiseDomainEvents(new BookingConfirmedDomainEvent(Id));
    }

    public void Reject()
    {
        if (Status != BookingStatus.Reserved)
        {
            throw new InvalidOperationException("only reserved bookings can be reject.");
        }

        if (Status == BookingStatus.Rejected)
        {
            throw new InvalidOperationException("bookings is already rejected.");
        }

        Status = BookingStatus.Rejected;
        RejectedAt = DateTime.Now;

        RaiseDomainEvents(new BookingRejectedDomainEvent(Id));
    }

    public void Cancel()
    {
        if (Status != BookingStatus.Confirmed)
        {
            throw new InvalidOperationException("only confirmed bookings can be Cancelled.");
        }

        if (Status == BookingStatus.Cancelled)
        {
            throw new InvalidOperationException("bookings is already cancelled.");
        }

        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        if (currentDate > DateRange.Start)
        {
            throw new InvalidOperationException("bookings is already started.");
        }

        Status = BookingStatus.Cancelled;
        CancelledAt = DateTime.Now;

        RaiseDomainEvents(new BookingCancelledDomainEvent(Id));
    }

    public void Complete()
    {
        if (Status != BookingStatus.Confirmed)
        {
            throw new InvalidOperationException("only confirmed bookings can be completed.");
        }

        Status = BookingStatus.Completed;
        CompletedAt = DateTime.Now;

        RaiseDomainEvents(new BookingCompletedDomainEvent(Id));
    }
}
