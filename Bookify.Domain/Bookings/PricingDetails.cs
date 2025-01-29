using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;

public record PricingDetails(Money PriceForPeriod, Money AmenitiesUpCharge, Money DiscountFee, Money TotalPrice);