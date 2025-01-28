using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions
{
    public abstract class Entity(Guid id, DateTime createdAt)
    {
        public Guid Id { get; init; } = id;

        public DateTime CreatedAt { get; init; } = createdAt;
    }
}
