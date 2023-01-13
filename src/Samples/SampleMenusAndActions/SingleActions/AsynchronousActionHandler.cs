using InteractiveCLI.Actions;

namespace SampleMenusAndActions.SingleActions;

public class AsynchronousActionHandler : SingleAsyncActionHandler<AsynchronousAction>
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

public class AsynchronousAction : SingleAction
{
}