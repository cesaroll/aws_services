/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
namespace Consumer.SQS.Config;

public class QueueSettings
{
    public const string Key = "Queue";
    public required string Name { get; set; }
}
