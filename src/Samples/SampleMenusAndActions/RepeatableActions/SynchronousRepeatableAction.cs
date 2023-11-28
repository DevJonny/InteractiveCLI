using InteractiveCLI.Actions;
using Serilog;
using Spectre.Console;

namespace SampleMenusAndActions.RepeatableActions;

public class SynchronousRepeatableAction : RepeatableAction
{
    private readonly ILogger _logger;

    public SynchronousRepeatableAction(ILogger logger)
    {
        _logger = logger.ForContext<SynchronousRepeatableAction>();
    }

    protected override bool RepeatableSyncAction()
    {
        var chosenOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to keep going?")
                .AddChoices("Yes", "No"));
        
        _logger.Information("You have chosen {Option}", chosenOption);

        return chosenOption is "No";
    }
}