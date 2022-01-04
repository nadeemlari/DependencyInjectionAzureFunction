using DependencyInjectionAzureFunction.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly:FunctionsStartup(typeof(DependencyInjectionAzureFunction.Startup))]
namespace DependencyInjectionAzureFunction;

public class Startup :FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<IMessageProcessor, MessageProcessorService>();
        builder.Services.AddLogging();
    }
}