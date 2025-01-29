using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews.Events;

namespace Bookify.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(Guid id,
            DateTime createdAt,
            Guid bookingId,
            Guid apartmentId,
            Guid userId,
            string comment,
            Score score) : base(id, createdAt)
        {
            BookingId = bookingId;
            ApartmentId = apartmentId;
            UserId = userId;
            Comment = comment;
            Score = score;
        }

        public Guid BookingId { get; private set; }

        public Guid ApartmentId { get; private set; }

        public Guid UserId { get; private set; }

        public string Comment { get; private set; }

        public Score Score { get; private set; }

        public static Result<Review> Create(Booking booking,
            string comment,
            Score score)
        {
            if (booking.Status != BookingStatus.Completed)
            {
                return Result.Failure<Review>(ReviewErrors.BookingNotCompleted);
            }

            var review = new Review(Guid.CreateVersion7(), DateTime.Now, booking.Id, booking.ApartmentId, booking.UserId, comment, score);

            review.RaiseDomainEvents(new ReviewCreatedDomainEvent(review.Id));

            return Result.Success(review);
        }
    }
}
