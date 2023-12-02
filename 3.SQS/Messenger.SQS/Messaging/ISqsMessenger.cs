/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Amazon.SQS.Model;

namespace Messenger.SQS.Messaging;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message, string operation, CancellationToken cancellationToken = default);
}
