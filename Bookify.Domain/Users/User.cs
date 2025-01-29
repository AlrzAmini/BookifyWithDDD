using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Shared;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id,
            DateTime createdAt,
            string firstName,
            string lastName,
            Email email) : base(id, createdAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public Email Email { get; private set; }

        public static Result<User> Create(string firstName, string lastName, Email email)
        {
            var user = new User(Guid.CreateVersion7(), DateTime.Now, firstName, lastName, email);

            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));

            return Result.Success(user);
        }
    }
}
