/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Persistence.PG.Entities;
using Riok.Mapperly.Abstractions;

namespace Persistence.PG.Mappers;

[Mapper]
public partial class CustomerMapper
{
    public static CustomerMapper Instance { get; } = new CustomerMapper();

    public CustomerEntity CustomerToCustomerEntity(Customer customer)
    {
        var entity = MapCustomerToCustomerEntity(customer);
        entity.DateOfBirth = entity.DateOfBirth.ToUniversalTime();
        return entity;
    }
    private partial CustomerEntity MapCustomerToCustomerEntity(Customer customer);
    public partial Customer CustomerEntityToCustomer(CustomerEntity customerEntity);
    public partial IEnumerable<Customer> CustomerEntitiesToCustomers(IEnumerable<CustomerEntity> customerEntities);
}
