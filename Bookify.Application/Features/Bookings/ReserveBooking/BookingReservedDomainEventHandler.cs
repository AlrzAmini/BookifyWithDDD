﻿using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Features.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler(IBookingRepository bookingRepository,
    IUserRepository userRepository,
    IApartmentRepository apartmentRepository,
    IEmailService emailService) : INotificationHandler<BookingReservedDomainEvent>
{
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetById(notification.BookingId, cancellationToken);
        if (booking is null)
        {
            return;
        }

        var user = await userRepository.GetById(booking.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        var apartment = await apartmentRepository.GetById(booking.ApartmentId, cancellationToken);
        if (apartment is null)
        {
            return;
        }

        await emailService.Send(
            new EmailDto(user.Email,
                $"Booking reserved for {apartment.Name}!",
                "You have 10 minutes to confirm your booking."),
            cancellationToken);
    }
}