using InteractiveCLI.Actions;
using Spectre.Console;

namespace SampleMenusAndActions.RepeatableActions;

public class SynchronousRepeatableActionHandler : RepeatableActionHandler<SynchronousRepeatableAction>
{
    protected override void RepeatableAction(out bool stop)
    {
        var chosenOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to keep going?")
                .AddChoices("Yes", "No"));

        stop = chosenOption is "No";
    }
}

public class SynchronousRepeatableAction : RepeatableAction
{
}