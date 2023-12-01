/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using LanguageExt.Common;

namespace App.Services;

public interface ICustomerService
{
    Task<Result<Customer>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Customer>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Result<Customer>> UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
