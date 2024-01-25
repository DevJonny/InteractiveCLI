using InteractiveCLI.Actions;
using Serilog;

namespace SimpleApp.SingleActions;

public class NonMenuAction(ILogger logger) : SingleAction
{
    private readonly ILogger _logger = logger.ForContext<NonMenuAction>();

    protected override void SingleSyncAction()
    {
        _logger.Information("Triggered an action not from a menu");
    }
}