namespace InteractiveCLI.Actions;

public abstract class SingleActionAsync : IAmAnAsyncAction
{
    public async Task DoAsync(CancellationToken cancellationToken = default)
    {
        await SingleAsyncAction();
    }

    protected abstract Task SingleAsyncAction();
    
}