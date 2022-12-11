using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class SingleActionBase<T> : RequestHandlerAsync<T> where T : Command
{
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        Action();
        
        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract void Action();
}