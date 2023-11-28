namespace InteractiveCLI.Actions;

public abstract class RepeatableAction : IAmAnSyncAction
{
    public void Do()
    {
        bool stop;

        do { stop = RepeatableSyncAction(); } while (!stop);
    }
    protected abstract bool RepeatableSyncAction();
}