using InteractiveCLI.Menus;
using Microsoft.Extensions.Logging;
using SampleMenusAndActions.Options;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;

namespace SampleMenusAndActions.Menus;

public class TopLevelMenu(IServiceProvider serviceProvider, InteractiveOptions options, ILogger<TopLevelMenu> logger) 
    : Menu(serviceProvider, true, true)
{
    protected override void BuildMenu()
    {
        logger.LogInformation("Hello, {Name}", options.Name);
        
        MenuBuilder
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu with asynchronous actions")
            .AddMenuItem<SynchronousAction>("Synchronous Action", "A synchronous one time action")
            .AddMenuItem<SynchronousRepeatableAction>("Repeatable Synchronous Action", "A synchronous repeatable action");
    }
}