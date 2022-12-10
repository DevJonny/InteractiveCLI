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
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu");
    }
}

public class TopLevelMenu : Command
{
    public TopLevelMenu() : base(Guid.NewGuid())
    {
    }
}