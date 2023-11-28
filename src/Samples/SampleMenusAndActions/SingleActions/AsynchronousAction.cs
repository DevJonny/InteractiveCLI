using InteractiveCLI.Actions;

namespace SampleMenusAndActions.SingleActions;

public class AsynchronousAction : SingleActionAsync
{
    protected override async Task SingleAsyncAction()
    {
        Console.WriteLine($"Running the {nameof(AsynchronousAction)}");

        await Task.FromResult(() =>
        {
            // Do something asynchronous
        });
    }
}