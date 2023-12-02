/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Amazon.SQS;
using Messenger.SQS.Config;
using Messenger.SQS.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.SQS.Config;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSqsMessenger(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
        services.AddScoped<ISqsMessenger, SqsMessenger>();

        return services;
    }
}
