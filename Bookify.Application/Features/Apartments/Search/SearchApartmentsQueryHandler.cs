using Bookify.Application.Abstractions.Database;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Features.Apartments.Dtos;
using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Application.Features.Apartments.Search;

internal sealed class SearchApartmentsQueryHandler(IApplicationDbContext context) : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponseDto>>
{
    public async Task<Result<IReadOnlyList<ApartmentResponseDto>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        var query = context.Apartments
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(a => a.Name.Contains(request.Name));
        }

        if (!string.IsNullOrWhiteSpace(request.Address))
        {
            query = query.Where(a => a.Address.State.Contains(request.Address) ||
                                     a.Address.City.Contains(request.Address) ||
                                     a.Address.Street.Contains(request.Address));
        }

        if (request.StartPrice is not null)
        {
            query = query.Where(a => a.Price.Amount >= request.StartPrice);
        }

        if (request.EndPrice is not null)
        {
            query = query.Where(a => a.Price.Amount <= request.EndPrice);
        }

        var result = await query
            .Select(a => new ApartmentResponseDto
            {
                Name = a.Name,
                Description = a.Description,
                Address = a.Address,
                Price = a.Price,
                Rules = a.Rules,
                Amenities = a.Amenities
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success<IReadOnlyList<ApartmentResponseDto>>(result);
    }
}