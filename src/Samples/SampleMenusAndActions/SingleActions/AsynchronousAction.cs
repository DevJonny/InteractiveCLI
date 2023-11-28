using InteractiveCLI.Actions;
using Serilog;

namespace SampleMenusAndActions.SingleActions;

public class AsynchronousAction(ILogger logger) : SingleActionAsync
{
    private readonly ILogger _logger = logger.ForContext<AsynchronousAction>();

    protected override async Task SingleAsyncAction()
    {
        _logger.Information($"Running the {nameof(AsynchronousAction)}");

        await Task.FromResult(() =>
        {
            // Do something asynchronous
        });
    }
}