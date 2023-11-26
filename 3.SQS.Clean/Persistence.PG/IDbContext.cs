/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.PG.Entities;

namespace Persistence.PG;

public interface IDbContext
{
    DbSet<CustomerEntity> Customers { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
