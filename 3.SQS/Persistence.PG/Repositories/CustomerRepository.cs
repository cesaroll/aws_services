/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.PG.Entities;
using Persistence.PG.Mappers;

namespace Persistence.PG.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbContext _dbContext;

    public CustomerRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Customers.AddAsync(customer.ToCustomerEntity(), cancellationToken);
        return entity.Entity.ToCustomer();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var customerEntities = await _dbContext.Customers.ToListAsync(cancellationToken);
        return customerEntities.ToCustomers();
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return customer?.ToCustomer();
    }

    public async Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var customerEntity = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id, cancellationToken);

        if (customerEntity == null)
            throw new KeyNotFoundException($"Customer with id {customer.Id} not found");

        customerEntity.FullName = customer.FullName;
        customerEntity.Email = customer.Email;
        customerEntity.GitHubUsername = customer.GitHubUsername;
        customerEntity.DateOfBirth = customer.DateOfBirth;

        _dbContext.Customers.Update(customerEntity);

        return customer;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customer == null)
            return false;

        _dbContext.Customers.Remove(customer);

        return true;
    }

}
