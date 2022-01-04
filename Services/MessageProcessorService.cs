using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionAzureFunction.Services;

public class MessageProcessorService: IMessageProcessor
{
    private readonly ILogger<MessageProcessorService> _logger;

    public MessageProcessorService(ILogger<MessageProcessorService> logger)
    {
        _logger = logger;
    }
    public Task ProcessAsync()
    {
        _logger.LogInformation("ProcessAsync called");
        return Task.CompletedTask;
    }
}