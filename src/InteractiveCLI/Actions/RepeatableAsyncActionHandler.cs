using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public abstract class RepeatableAsyncActionHandler<T> : RequestHandlerAsync<T> where T : RepeatableAction
{
    public T RepeatingAction { get; private set; }
    
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        RepeatingAction = command;
        
        bool stop;

        do
        {
            stop = await RepeatableAsyncAction();
        } while (!stop);

        return await base.HandleAsync(command, cancellationToken);
    }

    protected abstract Task<bool> RepeatableAsyncAction();
}