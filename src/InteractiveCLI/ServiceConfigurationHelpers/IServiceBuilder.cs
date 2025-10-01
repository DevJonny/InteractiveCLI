namespace InteractiveCLI.ServiceConfigurationHelpers;

public interface IServiceBuilder<TService> where TService : class
{
    public string Name { get; }
    public IServiceCollection Services { get; }
}
