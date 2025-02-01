using Bookify.Domain.Apartments;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Database;

namespace Bookify.Infrastructure.Features.Users;


internal sealed class UserRepository(ApplicationDbContext context)
    : Repository<User>(context), IUserRepository
{
   
}