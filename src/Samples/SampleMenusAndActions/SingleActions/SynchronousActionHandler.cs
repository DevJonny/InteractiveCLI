using InteractiveCLI.Actions;

namespace SampleMenusAndActions.SingleActions;

public class SynchronousActionHandler : SingleActionHandler<SynchronousAction>
{
    protected override void Action()
    {
    }
}

public class SynchronousAction : SingleAction
{
}