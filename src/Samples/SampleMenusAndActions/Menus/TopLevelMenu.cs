using InteractiveCLI.Menus;
using Microsoft.Extensions.Logging;
using Paramore.Brighter;
using SampleMenusAndActions.Options;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;

namespace SampleMenusAndActions.Menus;

public class TopLevelMenuHandler : MenuHandlerBase<TopLevelMenu>
{
    private readonly InteractiveOptions _options;
    private readonly ILogger<TopLevelMenuHandler> _logger;
    
    public TopLevelMenuHandler(
        IAmACommandProcessor commandProcessor, 
        InteractiveOptions options,
        ILogger<TopLevelMenuHandler> logger) : base(commandProcessor, isTopLevel: true)
    {
        _options = options;
        _logger = logger;
    }
    
    protected override void BuildMenu()
    {
        _logger.LogInformation("Hello, {Name}", _options.Name);
        
        MenuBuilder
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu with asynchronous actions")
            .AddMenuItem<SynchronousAction>("Synchronous Action", "A synchronous one time action")
            .AddMenuItem<SynchronousRepeatableAction>("Repeatable Synchronous Action", "A synchronous repeatable action");
    }
}

public class TopLevelMenu : Command
{
    public TopLevelMenu() : base(Guid.NewGuid())
    {
    }
}