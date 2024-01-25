using InteractiveCLI.Menus;
using SampleMenusAndActions.Options;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;
using Serilog;

namespace SampleMenusAndActions.Menus;

public class TopLevelMenu(InteractiveOptions options, ILogger logger) 
    : Menu(true, true)
{
    private readonly ILogger _logger = logger.ForContext<TopLevelMenu>();
    
    protected override void BuildMenu()
    {
        _logger.Information("Hello, {Name}", options.Name);
        
        new NonMenuAction(logger).Do();
        
        MenuBuilder
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu with asynchronous actions")
            .AddMenuItem<SynchronousAction>("Synchronous Action", "A synchronous one time action")
            .AddMenuItem<SynchronousRepeatableAction>("Repeatable Synchronous Action", "A synchronous repeatable action");
    }
}