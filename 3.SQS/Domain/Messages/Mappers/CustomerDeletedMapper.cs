/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Domain.Messages.Mappers;

[Mapper]
public partial class CustomerDeletedMapper
{
    public static CustomerDeletedMapper Instance { get; } = new CustomerDeletedMapper();

    public partial CustomerDeleted CustomerToCustomerDeleted(Customer customer);
}

public static class CustomerDeletedMapperExtensions
{
    public static CustomerDeleted ToCustomerDeleted(this Customer customer) =>
        CustomerDeletedMapper.Instance.CustomerToCustomerDeleted(customer);
}
