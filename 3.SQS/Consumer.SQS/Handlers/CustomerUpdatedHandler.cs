/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using System.Text.Json;
using Domain.Messages;
using MediatR;
using ILogger = Serilog.ILogger;

namespace Consumer.SQS.Handlers;

public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdated>
{
    private readonly ILogger _logger;

    public CustomerUpdatedHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CustomerUpdated request, CancellationToken cancellationToken)
    {
        _logger.Information("CustomerUpdated: {request}", JsonSerializer.Serialize(request));
        return Unit.Task;
    }
}
