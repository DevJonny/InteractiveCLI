using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class SingleAsyncActionHandler<T> : RequestHandlerAsync<T> where T : SingleAction
{
    protected T SingleAction { get; private set; }
    
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        SingleAction = command;
        
        await ActionAsync();
        
        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract Task ActionAsync();
}