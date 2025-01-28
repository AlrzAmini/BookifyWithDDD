namespace Bookify.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<int> SaveChanges(CancellationToken  cancellationToken = default);
}