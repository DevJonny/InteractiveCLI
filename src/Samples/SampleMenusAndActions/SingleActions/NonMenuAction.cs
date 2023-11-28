using InteractiveCLI.Actions;
using Serilog;

namespace SampleMenusAndActions.SingleActions;

public class NonMenuAction(ILogger logger) : SingleAction
{
    private readonly ILogger _logger = logger.ForContext<NonMenuAction>();

    protected override void SingleSyncAction()
    {
        _logger.Information("Triggered an action not from a menu");
    }
}