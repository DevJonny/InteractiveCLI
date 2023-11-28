using InteractiveCLI.Actions;
using Serilog;
using Spectre.Console;

namespace SampleMenusAndActions.RepeatableActions;

public class AsynchronousRepeatableAction : RepeatableActionAsync
{
    private readonly ILogger _logger;

    public AsynchronousRepeatableAction(ILogger logger)
    {
        _logger = logger.ForContext<AsynchronousRepeatableAction>();
    }

    protected override async Task<bool> RepeatableAsyncAction()
    {
        var chosenOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to doing some async thing?")
                .AddChoices("Yes", "No"));
        
        _logger.Information("You chose {Option}", chosenOption);

        return await Task.FromResult(chosenOption is "No");
    }
}