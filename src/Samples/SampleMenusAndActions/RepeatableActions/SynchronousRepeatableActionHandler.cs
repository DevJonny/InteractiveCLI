using InteractiveCLI.Actions;
using Paramore.Brighter;
using Spectre.Console;

namespace SampleMenusAndActions.RepeatableActions;

public class SynchronousRepeatableActionHandler : RepeatableActionBase<SynchronousRepeatableAction>
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

public class SynchronousRepeatableAction : Command
{
    public SynchronousRepeatableAction() : base(Guid.NewGuid())
    {
    }
}