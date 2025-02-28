﻿using FluentValidation;

namespace Bookify.Application.Features.Bookings.ReserveBooking;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.ApartmentId).NotEmpty();

        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.StartDate)
            .LessThan(c => c.EndDate);
    }
}