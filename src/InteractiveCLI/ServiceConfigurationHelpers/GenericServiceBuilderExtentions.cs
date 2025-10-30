namespace InteractiveCLI.ServiceConfigurationHelpers;

public static class GenericServiceBuilderExtensions
{
    public static IServiceBuilder<TService> ConfigureService<TService>(
        this IServiceBuilder<TService> builder,
        Action<ServiceFactoryOptions<TService>> configureOptions)
        where TService : class
    {
        builder.Services.Configure<ServiceFactoryOptions<TService>>(builder.Name, configureOptions);
        return builder;
    }

    public static IServiceBuilder<TService> ConfigureService<TService>(
        this IServiceBuilder<TService> builder,
        Action<ServiceFactoryOptions<TService>, IServiceProvider> configureOptions)
        where TService : class
    {
        builder.Services.Configure<ServiceFactoryOptions<TService>>(builder.Name, options =>
        {
            options.ServiceActions.Add((service, serviceProvider) =>
            {
                configureOptions(options, serviceProvider);
            });
        });
        return builder;
    }

    public static IServiceBuilder<TService> WithConstructorArgs<TService>(
        this IServiceBuilder<TService> builder,
        params object[] args)
        where TService : class
    {
        return builder.ConfigureService(options =>
        {
            options.ConstructorArgumentProviders.Add(_ => args);
        });
    }

    public static IServiceBuilder<TService> WithConstructorArgs<TService>(
        this IServiceBuilder<TService> builder,
        Func<IServiceProvider, object[]> argsProvider)
        where TService : class
    {
        return builder.ConfigureService(options =>
        {
            options.ConstructorArgumentProviders.Add(argsProvider);
        });
    }

    public static IServiceBuilder<TService> AddServiceAction<TService>(
        this IServiceBuilder<TService> builder,
        Action<TService, IServiceProvider> action)
        where TService : class
    {
        return builder.ConfigureService(options =>
        {
            options.ServiceActions.Add(action);
        });
    }

    public static IServiceBuilder<TService> WithCustomFactory<TService>(
        this IServiceBuilder<TService> builder,
        Func<IServiceProvider, object[], TService> customFactory)
        where TService : class
    {
        return builder.ConfigureService(options =>
        {
            options.CustomFactory = customFactory;
        });
    }

    public static IServiceBuilder<TService> WithCustomFactory<TService>(
        this IServiceBuilder<TService> builder,
        Func<IServiceProvider, TService> customFactory)
        where TService : class
    {
        return builder.ConfigureService(options =>
        {
            options.CustomFactory = (serviceProvider, _) => customFactory(serviceProvider);
        });
    }
}
