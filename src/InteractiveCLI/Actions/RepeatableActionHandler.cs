using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class RepeatableActionHandler<T> : RequestHandlerAsync<T> where T : RepeatableAction
{
    public T RepeatingAction { get; private set; }
    
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        RepeatingAction = command;
        
        bool stop;

        do
        {
            RepeatableAction(out stop);
        } while (!stop);

        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract void RepeatableAction(out bool stop);
}