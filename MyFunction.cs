using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using DependencyInjectionAzureFunction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionAzureFunction;

public class MyFunction
{
    private readonly IMessageProcessor _processor;
    private readonly ServiceBusClient _serviceBusClient;

    public MyFunction(IMessageProcessor processor, ServiceBusClient serviceBusClient)
    {
        _processor = processor;
        _serviceBusClient = serviceBusClient;
    }
    [FunctionName("MyFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req, ILogger log)
    {

        await _processor.ProcessAsync();
        var sender = _serviceBusClient.CreateSender("test");
        await sender.SendMessageAsync(new ServiceBusMessage("this is from azure function"));
        return new OkObjectResult("Executed Successfully");
    }
}