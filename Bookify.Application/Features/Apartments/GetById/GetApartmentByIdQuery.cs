using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Features.Apartments.Dtos;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Features.Apartments.GetById;

public record GetApartmentByIdQuery(Guid ApartmentId) : IQuery<ApartmentResponseDto>;