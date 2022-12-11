using InteractiveCLI.Menus;
using Paramore.Brighter;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;

namespace SampleMenusAndActions.Menus;

public class TopLevelMenuHandler : MenuHandlerBase<TopLevelMenu>
{
    public TopLevelMenuHandler(IAmACommandProcessor commandProcessor) : base(commandProcessor, isTopLevel: true)
    {
    }
    
    protected override void BuildMenu()
    {
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