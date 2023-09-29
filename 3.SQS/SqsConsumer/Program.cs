using Amazon.SQS;
using Amazon.SQS.Model;

var cts = new CancellationTokenSource();
var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
	QueueUrl = queueUrlResponse.QueueUrl
};

while (!cts.IsCancellationRequested)
{
	var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);
	
	response.Messages.ForEach(async message =>
	{
		Console.WriteLine($"Message received with id {message.MessageId}");
		Console.WriteLine($"Message body: {message.Body}");
		// Console.WriteLine($"Message attributes: {message.MessageAttributes}");
		// Console.WriteLine($"Message receipt handle: {message.ReceiptHandle}");

		var deleteMessageRequest = new DeleteMessageRequest
		{
			QueueUrl = queueUrlResponse.QueueUrl,
			ReceiptHandle = message.ReceiptHandle
		};
		
		await sqsClient.DeleteMessageAsync(deleteMessageRequest, cts.Token);
	});
	
	await Task.Delay(3000);
}