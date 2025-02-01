using Bookify.Domain.Apartments;
using Bookify.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(b => b.Id);

            builder.Property(user => user.FirstName)
                .HasMaxLength(200);

            builder.Property(user => user.LastName)
                .HasMaxLength(200);

            builder.Property(user => user.Email)
                .HasMaxLength(256);

            builder.HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
