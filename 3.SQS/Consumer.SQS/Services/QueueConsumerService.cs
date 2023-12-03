/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Consumer.SQS.Config;
using Domain.Messages;
using MediatR;
using Microsoft.Extensions.Options;
using ILogger = Serilog.ILogger;

namespace Consumer.SQS.Services;

public class QueueConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IOptions<QueueSettings> _queueSettings;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    private string? _queueUrl;

    public QueueConsumerService(IAmazonSQS sqsClient, IOptions<QueueSettings> queueSettings, ILogger logger, IMediator mediator)
    {
        _sqsClient = sqsClient;
        _queueSettings = queueSettings;
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await GetQueueUrlAsync(stoppingToken);

        var receiveMessageReguest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string> { "All" }
        };

        while(!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqsClient.ReceiveMessageAsync(receiveMessageReguest, stoppingToken);

            foreach (var message in response.Messages)
            {
               try
               {
                    var messageType = message.MessageAttributes["MessageType"].StringValue;
                    var type = Type.GetType($"Domain.Messages.{messageType}, Domain");

                    if (type is null)
                    {
                        _logger.Warning("Invalid MessageType: {messageType}", messageType);
                        continue;
                    }

                    //_logger.Information("Message: {message}", message.Body);

                    var messageObject = (IMessage)JsonSerializer.Deserialize(message.Body, type!)!;

                    await _mediator.Send(messageObject, stoppingToken);

                    await _sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle, stoppingToken);
               } catch(Exception ex)
               {
                     _logger.Error(ex, "Error processing message: {@message}", message);
                     continue;
               }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task<string> GetQueueUrlAsync(CancellationToken cancellationToken)
    {
        if (_queueUrl is not null)
            return _queueUrl;

        var queueUrlResponse = await _sqsClient.GetQueueUrlAsync(_queueSettings.Value.Name, cancellationToken);
        _queueUrl = queueUrlResponse.QueueUrl;

        return _queueUrl;
    }
}
