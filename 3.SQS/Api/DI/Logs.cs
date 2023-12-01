/*
 * @author: Cesar Lopez
 * @copyright 2023 - All rights reserved
 */
using Serilog;

namespace Api.DI;

public static class Logs
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext();
        });
    }
}
