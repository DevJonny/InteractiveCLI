using InteractiveCLI.Actions;
using Paramore.Brighter;

namespace SampleMenusAndActions.SingleActions;

public class SynchronousActionHandler : SingleActionBase<SynchronousAction>
{
    protected override void Action()
    {
    }
}

public class SynchronousAction : Command
{
    public SynchronousAction() : base(Guid.NewGuid())
    {
    }
}