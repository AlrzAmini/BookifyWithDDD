using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Features.Apartments.Dtos;

namespace Bookify.Application.Features.Apartments.Search;

public record SearchApartmentsQuery(string? Name, string? Address, decimal? StartPrice, decimal? EndPrice, DateOnly? StartDate, DateOnly? EndDate) : IQuery<IReadOnlyList<ApartmentResponseDto>>;