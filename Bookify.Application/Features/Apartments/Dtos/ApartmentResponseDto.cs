using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Features.Apartments.Dtos
{
    public sealed class ApartmentResponseDto
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public Address? Address { get; init; }

        public Money? Price { get; init; }

        public List<Rule> Rules { get; init; } = [];

        public List<Amenity> Amenities { get; init; } = [];

    }
}
