using Paramore.Brighter;
using Spectre.Console;

namespace InteractiveCLI.Menus;

public abstract class MenuHandlerBase<T> : RequestHandlerAsync<T> where T : class, IRequest
{
    protected readonly MenuBuilder MenuBuilder;

    private readonly IAmACommandProcessor _commandProcessor;
    
    protected MenuHandlerBase(IAmACommandProcessor commandProcessor)
    {
        _commandProcessor = commandProcessor;
        MenuBuilder = new MenuBuilder();
    }
    
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        for(;;)
        {
            MenuBuilder.MenuItems.Clear();
            
            BuildMenu();

            if (MenuBuilder.MenuItems.All(i => i.Name.ToLower() != "quit"))
                MenuBuilder.AddMenuItem<Back>("Back", "Go back up a level");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(MenuBuilder.MenuItems.Select(i => i.Name)));

            if (option is "Back" || option.ToLower() is "quit")
                return await base.HandleAsync(command, cancellationToken);

            if (option is "Help")
            {
                PrintHelp();
                continue;
            }
            
            // Create command and dispatch
            var commandType = MenuBuilder.MenuItems.First(i => i.Name == option).CommandType;
            var nextCommand = (dynamic) Convert.ChangeType(Activator.CreateInstance(commandType), commandType);
            await _commandProcessor.SendAsync(nextCommand, cancellationToken: cancellationToken);
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