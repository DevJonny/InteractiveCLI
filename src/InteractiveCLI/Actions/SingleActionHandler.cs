using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class SingleActionHandler<T> : RequestHandlerAsync<T> where T : SingleAction
{
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        Action();
        
        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract void Action();
}