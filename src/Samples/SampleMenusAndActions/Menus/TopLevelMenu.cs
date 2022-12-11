using InteractiveCLI.SingleActions;
using Paramore.Brighter;

namespace InteractiveCLI.Menus;

public class TopLevelMenuHandler : MenuHandlerBase<TopLevelMenu>
{
    public TopLevelMenuHandler(IAmACommandProcessor commandProcessor) : base(commandProcessor)
    {
    }
    
    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu")
            .AddMenuItem<SynchronousAction>("Synchronous Action", "A synchronous one time action");
    }
}

public class TopLevelMenu : Command
{
    public TopLevelMenu() : base(Guid.NewGuid())
    {
    }
}