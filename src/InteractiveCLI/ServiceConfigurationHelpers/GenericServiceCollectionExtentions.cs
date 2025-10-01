using Microsoft.Extensions.DependencyInjection.Extensions;

namespace InteractiveCLI.ServiceConfigurationHelpers;

public static class GenericServiceCollectionExtentions
{
    public static IServiceBuilder<TService> AddServiceWithFactory<TService>(
        this IServiceCollection services,
        string name = "Default")
        where TService : class
    {
        // Register the generic factory
        services.TryAddSingleton<IServiceFactory<TService>, DefaultServiceFactory<TService>>();

        // Register named options for this service type
        services.Configure<ServiceFactoryOptions<TService>>(name, options => { });

        // Register the service using the factory
        services.TryAddTransient<TService>(serviceProvider =>
        {
            var factory = serviceProvider.GetRequiredService<IServiceFactory<TService>>();
            return factory.CreateService(name);
        });

        return new DefaultServiceBuilder<TService>(services, name);
    }

    public static IServiceBuilder<TService> AddServiceWithFactory<TService, TImplementation>(
        this IServiceCollection services,
        string name = "Default")
        where TService : class
        where TImplementation : class, TService
    {
        // Register the generic factory
        services.TryAddSingleton<IServiceFactory<TService>, DefaultServiceFactory<TService>>();

        // Register named options
        services.Configure<ServiceFactoryOptions<TService>>(name, options =>
        {
            // Set custom factory to create TImplementation
            options.CustomFactory = (serviceProvider, args) =>
                (TService)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TImplementation), args);
        });

        // Register the service interface
        services.TryAddTransient<TService>(serviceProvider =>
        {
            var factory = serviceProvider.GetRequiredService<IServiceFactory<TService>>();
            return factory.CreateService(name);
        });

        // Also register the concrete type
        services.TryAddTransient<TImplementation>(serviceProvider =>
            (TImplementation)serviceProvider.GetRequiredService<TService>());

        return new DefaultServiceBuilder<TService>(services, name);
    }
}
