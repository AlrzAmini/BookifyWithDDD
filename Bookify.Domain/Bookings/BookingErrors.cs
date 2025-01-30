using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings
{
    public static class BookingErrors
    {
        public static readonly Error InvalidStatusForConfirmation =
            new(nameof(InvalidStatusForConfirmation), "Only reserved bookings can be confirmed.");

        public static readonly Error InvalidStatusForRejection =
            new(nameof(InvalidStatusForRejection), "Only reserved bookings can be rejected.");

        public static readonly Error AlreadyRejected =
            new(nameof(AlreadyRejected), "Booking is already rejected.");

        public static readonly Error InvalidStatusForCancellation =
            new(nameof(InvalidStatusForCancellation), "Only confirmed bookings can be cancelled.");

        public static readonly Error AlreadyCancelled =
            new(nameof(AlreadyCancelled), "Booking is already cancelled.");

        public static readonly Error BookingAlreadyStarted =
            new(nameof(BookingAlreadyStarted), "The booking has already started and cannot be cancelled.");

        public static readonly Error InvalidStatusForCompletion =
            new(nameof(InvalidStatusForCompletion), "Only confirmed bookings can be completed.");

        public static readonly Error Overlap =
            new(nameof(Overlap), "Booking is overlap with another booking.");
    }
}
