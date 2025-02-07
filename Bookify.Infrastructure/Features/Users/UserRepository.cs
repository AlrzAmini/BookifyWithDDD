using Bookify.Application.Abstractions.Database;
using Bookify.Domain.Apartments;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Database;

namespace Bookify.Infrastructure.Features.Users;


internal sealed class UserRepository(IApplicationDbContext context)
    : Repository<User>(context), IUserRepository
{
   
}