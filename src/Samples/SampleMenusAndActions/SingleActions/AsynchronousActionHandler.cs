using InteractiveCLI.Actions;
using Paramore.Brighter;

namespace SampleMenusAndActions.SingleActions;

public class AsynchronousActionHandler : SingleAsyncActionBase<AsynchronousAction>
{
    protected override async Task ActionAsync()
    {
        Console.WriteLine($"Running the {nameof(AsynchronousActionHandler)}");

        await Task.FromResult(() =>
        {
            // Do something asynchronous
        });
    }
}

public class AsynchronousAction : Command
{
    public AsynchronousAction() : base(Guid.NewGuid())
    {
    }
}