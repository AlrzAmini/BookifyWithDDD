using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Application.Abstractions.Database;

public interface IApplicationDbContext
{
    public DbSet<Apartment> Apartments { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Review> Reviews { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    void Dispose();
    ValueTask DisposeAsync();
    IQueryable<TResult> FromExpression<TResult>([NotNullAttribute] Expression<Func<IQueryable<TResult>>> expression);
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    DbSet<TEntity> Set<TEntity>([NotNullAttribute] string name) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}