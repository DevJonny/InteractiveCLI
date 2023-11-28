namespace InteractiveCLI.Actions;

public interface IAmAnAsyncAction : IAmAnAction
{
    Task DoAsync(CancellationToken cancellationToken = default);
}