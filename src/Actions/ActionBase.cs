namespace InteractiveCLI.Actions;

public abstract class ActionBase
{
    protected abstract IDictionary<string, (Func<Task<bool>> Action, string Description)> AvailableActions { get; }
        
    public abstract Task Run();

    protected async Task SelectOption(Func<Task> beforeOption, Func<string, Func<Task<bool>>> selectAction, Func<Task> afterOptions = null)
    {
        for (;;)
        {
            if (beforeOption is not null)
                await beforeOption();
                
            AvailableActions.Add("Back", (() => null, "Go back to the previous menu"));
            AvailableActions.Add("Quit", (() => null, "Quit the application"));
                
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(AvailableActions.Keys)
                    .AddChoices("Back", "Quit", "Help"));

            if (option is "Back" or "Quit")
                break; // For "Quit" do something better like invoke an event

            if (option is "Help")
            {
                PrintHelp();
                continue;
            }

            var action = selectAction(option);

            var goBackOnCompletion = await action.Invoke();
                
            if (goBackOnCompletion)
                break;
                
            if (afterOptions is null)
                continue;

            await afterOptions();
        }
    }

    private void PrintHelp()
    {
        var table = new Table();
        table.AddColumn("Option");
        table.AddColumn("Help Text");

        foreach (var (action, description) in AvailableActions.Values)
            table.AddRow(action.Method.Name, description);

        AnsiConsole.Write(table);
    }
}