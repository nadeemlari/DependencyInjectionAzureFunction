using System.Threading.Tasks;

namespace DependencyInjectionAzureFunction.Services;

public interface IMessageProcessor
{
    public Task ProcessAsync();
    
}