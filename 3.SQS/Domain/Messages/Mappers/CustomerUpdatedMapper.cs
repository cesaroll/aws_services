/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Domain.Messages.Mappers;

[Mapper]
public partial class CustomerUpdatedMapper
{
    public static CustomerUpdatedMapper Instance { get; } = new CustomerUpdatedMapper();

    public partial CustomerUpdated CustomerToCustomerUpdated(Customer customer);
}

public static class CustomerUpdatedMapperExtensions
{
    public static CustomerUpdated ToCustomerUpdated(this Customer customer) =>
        CustomerUpdatedMapper.Instance.CustomerToCustomerUpdated(customer);
}
