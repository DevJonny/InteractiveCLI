using InteractiveCLI.Actions;
using Spectre.Console;

namespace InteractiveCLI.Menus;

public abstract class Menu(bool quitable, bool isTopLevel) : IAmAnAsyncAction
{
    protected readonly MenuBuilder MenuBuilder = new();

    public async Task DoAsync(CancellationToken cancellationToken = default)
    {
        for (;;)
        {
            MenuBuilder.MenuItems.Clear();

            BuildMenu();

            if (!isTopLevel)
                MenuBuilder.AddMenuItem<Back>("Back", "Go back up a level");
            
            if (isTopLevel || quitable)
                MenuBuilder.AddMenuItem<Quit>("Quit", "Quit the application");
            
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(MenuBuilder.MenuItems.Select(i => i.Name)));

            if (option is "Back")
                break;

            if (option is "Quit")
                throw new StopApplicationAction();
            
            if (option is "Help")
            {
                PrintHelp();
                continue;
            }

            var actionType = MenuBuilder.MenuItems.First(mi => mi.Name == option).ActionType;

            var nextAction = InteractiveCliBootstrapper.ServiceProvider.GetService(actionType);
            
            if (nextAction is IAmAnAsyncAction nextAsyncAction)
                await nextAsyncAction.DoAsync(cancellationToken);
            else if (nextAction is IAmAnSyncAction nextSyncAction)
                nextSyncAction.Do();
        }
    }

    protected abstract void BuildMenu();
    
    private void PrintHelp()
    {
        var table = new Table();
        table.AddColumn("Option");
        table.AddColumn("Help Text");

        foreach (var menuItem in MenuBuilder.MenuItems)
            table.AddRow(menuItem.Name, menuItem.Description);
        
        AnsiConsole.Write(table);
    }
    
}