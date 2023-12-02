/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
namespace Persistence.PG.Config;

public class PgSettings
{
    public const string Key = "Customers";
    public required string ConnectionString { get; set; }
}
