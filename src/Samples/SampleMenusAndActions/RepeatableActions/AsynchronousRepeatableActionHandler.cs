using InteractiveCLI.Actions;
using Paramore.Brighter;
using Spectre.Console;

namespace SampleMenusAndActions.RepeatableActions;

public class AsynchronousRepeatableActionHandler : RepeatableAsyncActionBase<AsynchronousRepeatableAction>
{
    protected override async Task<bool> RepeatableAsyncAction()
    {
        var chosenOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to doing some async thing?")
                .AddChoices("Yes", "No"));

        return await Task.FromResult(chosenOption is "No");
    }
}

public class AsynchronousRepeatableAction : Command
{
    public AsynchronousRepeatableAction() : base(Guid.NewGuid())
    {
    }
}