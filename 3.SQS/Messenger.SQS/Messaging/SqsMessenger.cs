/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */

using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Messenger.SQS.Config;
using Microsoft.Extensions.Options;

namespace Messenger.SQS.Messaging;

public class SqsMessenger : ISqsMessenger
{
    private readonly IAmazonSQS _messengerClient;
    private readonly IOptions<QueueSettings> _queueSettings;

    private string? _queueUrl;

    public SqsMessenger(IAmazonSQS messengerClient, IOptions<QueueSettings> queueSettings)
    {
        _messengerClient = messengerClient;
        _queueSettings = queueSettings;
    }

    public async Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var queueUrl = await GetQueueUrlAsync(cancellationToken);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType",
                    new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _messengerClient.SendMessageAsync(sendMessageRequest, cancellationToken);
    }

    private async Task<string> GetQueueUrlAsync(CancellationToken cancellationToken)
    {
        if (_queueUrl is not null)
            return _queueUrl;

        var queueUrlResponse = await _messengerClient.GetQueueUrlAsync(_queueSettings.Value.Name, cancellationToken);
        _queueUrl = queueUrlResponse.QueueUrl;

        return _queueUrl;
    }
}
