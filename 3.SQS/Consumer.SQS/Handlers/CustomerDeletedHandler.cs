/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using System.Text.Json;
using Domain.Messages;
using MediatR;
using ILogger = Serilog.ILogger;

namespace Consumer.SQS.Handlers;

public class CustomerDeletedHandler : IRequestHandler<CustomerDeleted>
{
    private readonly ILogger _logger;

    public CustomerDeletedHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        _logger.Information("CustomerDeleted: {request}", JsonSerializer.Serialize(request));
        return Unit.Task;
    }
}
