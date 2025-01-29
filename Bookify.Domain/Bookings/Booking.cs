using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;

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
        BookingStatus bookingStatus) : base(id, createdAt)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        DateRange = dateRange;
        PriceForPeriod = priceForPeriod;
        AmenitiesUpCharge = amenitiesUpCharge;
        Status = bookingStatus;
    }

    public Guid ApartmentId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange DateRange { get; private set; }

    public Money PriceForPeriod { get; private set; }

    public Money AmenitiesUpCharge { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? RejectedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public static Booking Create(
        Guid apartmentId,
        Guid userId,
        DateRange dateRange,
        Money priceForPeriod,
        Money amenitiesUpCharge)
    {
        var booking = new Booking(
            Guid.CreateVersion7(),
            DateTime.Now,
            apartmentId,
            userId,
            dateRange,
            priceForPeriod,
            amenitiesUpCharge,
            BookingStatus.Pending);

        booking.RaiseDomainEvents(new BookingCreatedDomainEvent(booking.Id));

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
