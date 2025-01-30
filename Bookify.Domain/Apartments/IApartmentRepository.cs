using Bookify.Domain.Users;

namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<string?> GetNameById(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> Add(Apartment apartment, CancellationToken cancellationToken = default);
}