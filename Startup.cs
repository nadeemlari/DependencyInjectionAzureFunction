using Azure.Identity;
using DependencyInjectionAzureFunction.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
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
           cfg.AddServiceBusClientWithNamespace("mnl2022.servicebus.windows.net")
                .WithCredential(new AzureCliCredential());
        });
    }
}