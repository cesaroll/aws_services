/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using System.Text.Json;
using Domain.Messages;
using MediatR;
using ILogger = Serilog.ILogger;

namespace Consumer.SQS.Handlers;

public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
{
    private readonly ILogger _logger;

    public CustomerCreatedHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CustomerCreated request, CancellationToken cancellationToken)
    {
        _logger.Information("CustomerCreated: {request}", JsonSerializer.Serialize(request));

        return Unit.Task;
    }
}
