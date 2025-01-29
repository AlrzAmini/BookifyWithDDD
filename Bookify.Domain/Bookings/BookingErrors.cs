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
            new("InvalidStatusForConfirmation", "Only reserved bookings can be confirmed.");

        public static readonly Error InvalidStatusForRejection =
            new("InvalidStatusForRejection", "Only reserved bookings can be rejected.");

        public static readonly Error AlreadyRejected =
            new("AlreadyRejected", "Booking is already rejected.");

        public static readonly Error InvalidStatusForCancellation =
            new("InvalidStatusForCancellation", "Only confirmed bookings can be cancelled.");

        public static readonly Error AlreadyCancelled =
            new("AlreadyCancelled", "Booking is already cancelled.");

        public static readonly Error BookingAlreadyStarted =
            new("BookingAlreadyStarted", "The booking has already started and cannot be cancelled.");

        public static readonly Error InvalidStatusForCompletion =
            new("InvalidStatusForCompletion", "Only confirmed bookings can be completed.");
    }
}
