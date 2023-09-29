﻿using System;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Sqs.Models;;

var sqsClient = new AmazonSQSClient();

var customer = new CustomerCreated()
{
    Id = Guid.NewGuid(),
    Email = "cesarlerdeut@gmail.com",
    FullName = "Cesar Lerdeut",
    DateOfBirth = new DateTime(1980,1,1),
    GitHubUsername = "cesaroll"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendmessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CustomerCreated)
            }
        }
    }
};

var sendMessageResponse = await sqsClient.SendMessageAsync(sendmessageRequest);

Console.WriteLine($"Message sent with id {sendMessageResponse.MessageId}");