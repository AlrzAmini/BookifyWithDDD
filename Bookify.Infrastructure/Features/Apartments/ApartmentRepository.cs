﻿using Bookify.Application.Abstractions.Database;
using Bookify.Domain.Apartments;
using Bookify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Features.Apartments;

public sealed class ApartmentRepository(IApplicationDbContext context)
    : Repository<Apartment>(context), IApartmentRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<string?> GetNameById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Apartments
            .Where(a => a.Id == id)
            .Select(a => a.Name)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}