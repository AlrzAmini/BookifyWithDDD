using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public class PricingService
    {
        public PricingDetails CalculatePrice(Apartment apartment, DateRange dateRange, Money discountFee)
        {
            var currency = apartment.Price.Currency;

            if (discountFee.Amount > 0 && discountFee.Currency != currency)
            {
                throw new ApplicationException("price and discount currencies doesn't match");
            }

            var priceForPeriod = apartment.Price with { Currency = currency };

            decimal percentageUpCharge = default;
            foreach (var amenity in apartment.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.AirConditioning => 0.05m,
                    Amenity.Gym => 0.07m,
                    _ => 0.01m
                };
            }

            var amenitiesUpCharge = Money.Zero(currency);
            if (percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
            }

            var totalPrice = new Money(priceForPeriod.Amount + amenitiesUpCharge.Amount - discountFee.Amount, currency);

            return new PricingDetails(priceForPeriod, amenitiesUpCharge, discountFee, totalPrice);
        }
    }
}
