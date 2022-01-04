using System.IO;
using Azure.Identity;
using DependencyInjectionAzureFunction.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(DependencyInjectionAzureFunction.Startup))]

namespace DependencyInjectionAzureFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<IMessageProcessor, MessageProcessorService>();
        builder.Services.AddLogging();

        builder.Services.AddAzureClients(cfg =>
        {
            var ns = builder.GetContext().Configuration.GetValue<string>("ServiceBus:NameSpace");
            
            cfg.AddServiceBusClientWithNamespace(ns)
                .WithCredential(new AzureCliCredential());
        });
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);
        var context = builder.GetContext();
        builder.ConfigurationBuilder
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true,
                reloadOnChange: false)
            .AddEnvironmentVariables();
    }
}