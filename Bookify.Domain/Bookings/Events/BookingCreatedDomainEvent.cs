using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingCreatedDomainEvent(Guid BookingId) : IDomainEvent;