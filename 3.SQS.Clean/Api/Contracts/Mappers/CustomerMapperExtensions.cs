/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using Domain.Models;

namespace Api.Contracts.Mappers;

public static class CustomerMapperExtensions
{
    public static Customer ToCustomer(this CreateCustomerContract createCustomerContract) =>
        CustomerMapper.Instance.CreateCustomerContractToCustomer(createCustomerContract);
}
