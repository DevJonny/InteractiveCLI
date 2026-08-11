namespace InteractiveCLI.ServiceConfigurationHelpers;

public interface IServiceFactory<TService> where TService : class
{
    TService CreateService(string name = "Default");
}
