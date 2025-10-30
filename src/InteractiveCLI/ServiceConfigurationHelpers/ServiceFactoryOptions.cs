namespace InteractiveCLI.ServiceConfigurationHelpers;

public class ServiceFactoryOptions<TService> where TService : class
{
    public List<Action<TService, IServiceProvider>> ServiceActions { get; set; } = new();
    public IList<Func<IServiceProvider, object[]>> ConstructorArgumentProviders { get; private set; } = new List<Func<IServiceProvider, object[]>>();
    public Func<IServiceProvider, object[], TService>? CustomFactory { get; set; }
}
