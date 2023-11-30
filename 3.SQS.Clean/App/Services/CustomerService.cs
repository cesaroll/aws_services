/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using Domain.Models;
using LanguageExt.Common;
using Persistence;
using ILogger = Serilog.ILogger;

namespace App.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ILogger logger)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Customer>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try {
            var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

            if(customer == null)
            {
                _logger.Information("Customer with id: {id} not found", id);
                return new Result<Customer>(new KeyNotFoundException($"Customer with id {id} not found"));
            }

            _logger.Information("Getting: {@customer}", customer);

            return customer;

        } catch(Exception ex) {
            _logger.Error(ex, "Error getting customer with id: {id}", id);
            return new Result<Customer>(ex);
        }
    }


    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            _logger.Information("Getting all customers");

            return new Result<IEnumerable<Customer>>(customers);

        } catch(Exception ex) {
            _logger.Error(ex, "Error getting all customers");
            return new Result<IEnumerable<Customer>>(ex);
        }
    }

    public async Task<Result<Customer>> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try {
            var result = await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.Information("Created: {@Customer}", customer);

            return result;

        } catch(Exception ex) {
            _logger.Error(ex, "Error creating: {@Customer}", customer);
            return new Result<Customer>(ex);
        }
    }

    public async Task<Result<Customer>> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _customerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.Information("Updating: {@customer}", customer);

            return result;

        } catch(Exception ex) {
            _logger.Error(ex, "Error updating: {@customer}", customer);
            return new Result<Customer>(ex);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try {
            var result = await _customerRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.Information("Deleting Customer: {@id}", id);

            return result;

        } catch(Exception ex) {
            _logger.Error(ex, "Error deleting: {@id}", id);
            return new Result<bool>(ex);
        }
    }

}
