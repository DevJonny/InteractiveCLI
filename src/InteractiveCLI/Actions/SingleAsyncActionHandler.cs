using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class SingleAsyncActionHandler<T> : RequestHandlerAsync<T> where T : SingleAction
{
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        await ActionAsync();
        
        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract Task ActionAsync();
}