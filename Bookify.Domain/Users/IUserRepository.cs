using Bookify.Domain.Apartments;

namespace Bookify.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> Add(User user, CancellationToken cancellationToken = default);
}