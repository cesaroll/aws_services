/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Persistence.PG.Entities;

namespace Persistence.PG.Mappers;

public static class CustomerMapperExtensions
{
    public static CustomerEntity ToCustomerEntity(this Customer customer) =>
        CustomerMapper.Instance.CustomerToCustomerEntity(customer);

    public static Customer ToCustomer(this CustomerEntity customerEntity) =>
        CustomerMapper.Instance.CustomerEntityToCustomer(customerEntity);

    public static IEnumerable<Customer> ToCustomers(this IEnumerable<CustomerEntity> customerEntities) =>
        CustomerMapper.Instance.CustomerEntitiesToCustomers(customerEntities);
}
