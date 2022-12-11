using Paramore.Brighter;

namespace InteractiveCLI.SingleActions;

public class SynchronousActionHandler : SingleActionBase<SynchronousAction>
{
    protected override void Action()
    {
        Console.WriteLine($"Running the {nameof(SynchronousActionHandler)}");
    }
}

public class SynchronousAction : Command
{
    public SynchronousAction() : base(Guid.NewGuid())
    {
    }
}