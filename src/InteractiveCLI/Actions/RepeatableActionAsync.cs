namespace InteractiveCLI.Actions;

public abstract class RepeatableActionAsync : IAmAnAsyncAction
{
    public async Task DoAsync(CancellationToken cancellationToken = default)
    {
        bool stop;

        do { stop = await RepeatableAsyncAction(); } while (!stop);
    }

    protected abstract Task<bool> RepeatableAsyncAction();
}