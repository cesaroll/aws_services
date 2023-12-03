/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Domain.Messages.Mappers;

[Mapper]
public partial class CustomerCreatedMapper
{
    public static CustomerCreatedMapper Instance { get; } = new CustomerCreatedMapper();

    public partial CustomerCreated CustomerToCustomerCreated(Customer customer);
}

public static class CustomerCreatedMapperExtensions
{
    public static CustomerCreated ToCustomerCreated(this Customer customer) =>
        CustomerCreatedMapper.Instance.CustomerToCustomerCreated(customer);
}
