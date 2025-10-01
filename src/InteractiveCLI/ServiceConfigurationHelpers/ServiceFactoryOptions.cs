namespace InteractiveCLI.ServiceConfigurationHelpers;

public class ServiceFactoryOptions<TService> where TService : class
{
    public List<Action<TService, IServiceProvider>> ServiceActions { get; set; } = new();
    public List<Func<IServiceProvider, object[]>> ConstructorArgumentProviders { get; set; } = new();
    public Func<IServiceProvider, object[], TService>? CustomFactory { get; set; }
}
