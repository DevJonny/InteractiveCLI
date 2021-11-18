namespace InteractiveCLI.Actions;

public class Runner
{
    readonly IServiceProvider _serviceProvider;

    public Runner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Run<T>() where T : ActionBase
    {
        var service = _serviceProvider.GetService(typeof(T)) as T;

        if (service is null)
        {
            throw new InvalidOperationException();
        }

        await service.Run();
    }
}