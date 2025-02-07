using Bookify.Application.Abstractions.Database;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Database;

namespace Bookify.Infrastructure;

public abstract class Repository<T>(IApplicationDbContext context) where T : Entity
{
    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context
            .Set<T>()
            .FindAsync(id);
    }

    public async Task<Guid> Add(T entity, CancellationToken cancellationToken)
    {
        await context
           .Set<T>()
           .AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}