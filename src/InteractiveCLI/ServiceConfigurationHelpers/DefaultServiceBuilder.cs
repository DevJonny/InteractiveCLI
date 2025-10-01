namespace InteractiveCLI.ServiceConfigurationHelpers;

public class DefaultServiceBuilder<TService>(IServiceCollection services, string name) : IServiceBuilder<TService> where TService : class
{
    public string Name { get; } = name;
    public IServiceCollection Services { get; } = services;
}
