/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
namespace Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
