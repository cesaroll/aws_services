/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using System.Runtime.CompilerServices;
using Domain.Models;
using LanguageExt.Common;
using Persistence;

namespace App.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Customer>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

            if(customer == null)
                return new Result<Customer>(new KeyNotFoundException($"Customer with id {id} not found"));

            return customer;

        } catch(Exception ex) {
            return new Result<Customer>(ex);
        }
    }


    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            return new Result<IEnumerable<Customer>>(customers);

        } catch(Exception ex) {
            return new Result<IEnumerable<Customer>>(ex);
        }
    }

    public async Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try {
            var result = await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;

        } catch(Exception ex) {
            return new Result<Customer>(ex);
        }
    }

    public async Task<Result<Customer>> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _customerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;

        } catch(Exception ex) {
            return new Result<Customer>(ex);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try {
            var result = await _customerRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;

        } catch(Exception ex) {
            return new Result<bool>(ex);
        }
    }

}
