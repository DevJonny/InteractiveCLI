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
        var service = (T) _serviceProvider.GetService(typeof(T));

        await service.Run();
    }
}