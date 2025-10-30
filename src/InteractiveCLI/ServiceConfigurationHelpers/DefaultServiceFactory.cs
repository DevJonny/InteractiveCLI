using Microsoft.Extensions.Options;

namespace InteractiveCLI.ServiceConfigurationHelpers;

public class DefaultServiceFactory<TService>(
    IServiceProvider serviceProvider,
    IOptionsMonitor<ServiceFactoryOptions<TService>> optionsMonitor) : IServiceFactory<TService> where TService : class
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IOptionsMonitor<ServiceFactoryOptions<TService>> _optionsMonitor = optionsMonitor;

    public TService CreateService(string name = "Default")
    {
        var options = _optionsMonitor.Get(name);

        TService service;

        if (options.CustomFactory != null)
        {
            // Use custom factory if provided
            var args = GatherConstructorArguments(options);
            service = options.CustomFactory(_serviceProvider, args);
        }
        else
        {
            // Use default creation with ActivatorUtilities
            var args = GatherConstructorArguments(options);
            service = (TService)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TService), args);
        }

        // Apply registered service actions
        foreach (var action in options.ServiceActions)
        {
            action(service, _serviceProvider);
        }

        return service;
    }

    private object[] GatherConstructorArguments(ServiceFactoryOptions<TService> options)
    {
        var allArgs = new List<object>();

        foreach (var argProvider in options.ConstructorArgumentProviders)
        {
            var args = argProvider(_serviceProvider);
            allArgs.AddRange(args);
        }

        return allArgs.ToArray();
    }
}
