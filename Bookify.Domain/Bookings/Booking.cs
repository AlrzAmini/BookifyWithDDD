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

    //public void ConfirmBooking()
    //{
    //    if (Status != BookingStatus.Pending)
    //    {
    //        throw new InvalidOperationException("Only pending bookings can be confirmed.");
    //    }

    //    Status = BookingStatus.Confirmed;
    //    RaiseDomainEvents(new BookingConfirmedDomainEvent(Id));
    //}

    //public void CancelBooking()
    //{
    //    if (Status == BookingStatus.Canceled)
    //    {
    //        throw new InvalidOperationException("The booking is already canceled.");
    //    }

    //    Status = BookingStatus.Canceled;
    //    RaiseDomainEvents(new BookingCanceledDomainEvent(Id));
    //}

    //public void UpdateAmenitiesUpCharge(Money newUpCharge)
    //{
    //    if (newUpCharge == null || newUpCharge.Amount < 0)
    //    {
    //        throw new ArgumentException("Amenities upcharge must be a valid non-negative value.");
    //    }

    //    AmenitiesUpCharge = newUpCharge;
    //    RaiseDomainEvents(new BookingAmenitiesUpdatedDomainEvent(Id, newUpCharge));
    //}
}
