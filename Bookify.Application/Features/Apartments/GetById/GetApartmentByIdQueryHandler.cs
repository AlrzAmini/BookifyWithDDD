using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Features.Apartments.Dtos;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;

namespace Bookify.Application.Features.Apartments.GetById;

internal sealed class GetApartmentByIdQueryHandler(IApartmentRepository apartmentRepository) : IQueryHandler<GetApartmentByIdQuery, ApartmentResponseDto>
{
    public async Task<Result<ApartmentResponseDto>> Handle(GetApartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var apartment = await apartmentRepository.GetById(request.ApartmentId, cancellationToken);
        if (apartment is null)
        {
            return Result.Failure<ApartmentResponseDto>(ApartmentErrors.NotFound);
        }

        return Result.Success(new ApartmentResponseDto
        {
            Name = apartment.Name,
            Description = apartment.Description,
            Price = apartment.Price,
            Address = apartment.Address,
            Rules = apartment.Rules,
            Amenities = apartment.Amenities
        });
    }
}