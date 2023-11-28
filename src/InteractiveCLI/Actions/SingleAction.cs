namespace InteractiveCLI.Actions;

public abstract class SingleAction : IAmAnSyncAction
{
    public void Do()
    {
        SingleSyncAction();
    }

    protected abstract void SingleSyncAction();
}