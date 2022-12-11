using Paramore.Brighter;
using Spectre.Console;

namespace InteractiveCLI.Menus;

public abstract class MenuHandlerBase<T> : RequestHandlerAsync<T> where T : class, IRequest
{
    protected readonly MenuBuilder MenuBuilder;

    private readonly IAmACommandProcessor _commandProcessor;
    private readonly bool _quitable;
    private readonly bool _isTopLevel;
    
    protected MenuHandlerBase(IAmACommandProcessor commandProcessor, bool quitable = false, bool isTopLevel = false)
    {
        _commandProcessor = commandProcessor;
        _quitable = quitable;
        _isTopLevel = isTopLevel;
        
        MenuBuilder = new MenuBuilder();
    }
    
    public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = new())
    {
        for(;;)
        {
            MenuBuilder.MenuItems.Clear();
            
            BuildMenu();

            if (!_isTopLevel)
                MenuBuilder.AddMenuItem<Back>("Back", "Go back up a level");

            if (_isTopLevel || _quitable)
                MenuBuilder.AddMenuItem<Quit>("Quit", "Quit the application");

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(MenuBuilder.MenuItems.Select(i => i.Name)));

            if (option is "Back")
                return await base.HandleAsync(command, cancellationToken);
            
            if (option is "Quit")
                throw new StopApplicationAction();

            if (option is "Help")
            {
                PrintHelp();
                continue;
            }
            
            // Create command and dispatch
            var commandType = MenuBuilder.MenuItems.First(i => i.Name == option).CommandType;
            var nextCommand = (dynamic) Convert.ChangeType(Activator.CreateInstance(commandType), commandType)!;
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