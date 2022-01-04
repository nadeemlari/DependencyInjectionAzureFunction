using System.Threading.Tasks;
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

    public MyFunction(IMessageProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("MyFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req, ILogger log)
    {

        await _processor.ProcessAsync();
        return new OkObjectResult("Executed Successfully");
    }
}