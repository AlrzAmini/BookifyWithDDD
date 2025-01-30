using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new Error(nameof(NotFound), "User not found.");
    }
}
