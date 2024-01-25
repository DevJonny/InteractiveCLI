using InteractiveCLI.Actions;

namespace InteractiveCLI;

public static class ActionFactory
{
    public static T? Create<T>() where T : IAmAnAction
        => InteractiveCliBootstrapper.ServiceProvider.GetService<T>();
}