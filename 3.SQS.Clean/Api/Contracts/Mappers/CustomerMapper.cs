/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Api.Contracts.Mappers;

[Mapper]
public partial class CustomerMapper
{
    public static CustomerMapper Instance { get; } = new CustomerMapper();

    public Customer CreateCustomerContractToCustomer(CreateCustomerContract createCustomerContract)
    {
        var customer = createCustomerContract.ToCustomer();
        customer.Id = Guid.NewGuid();
        customer.DateOfBirth = customer.DateOfBirth.ToUniversalTime();
        return customer;
    }
    private partial Customer MapCreateCustomerContractToCustomer(CreateCustomerContract createCustomerContract);
}
