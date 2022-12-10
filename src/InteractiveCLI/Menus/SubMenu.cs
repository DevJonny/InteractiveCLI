using Paramore.Brighter;

namespace InteractiveCLI.Menus;

public class SubMenuHandler : MenuHandlerBase<SubMenu>
{
    public SubMenuHandler(IAmACommandProcessor commandProcessor) : base(commandProcessor)
    {
    }

    protected override void BuildMenu()
    {
    }
}

public class SubMenu : Command
{
    public SubMenu() : base(Guid.NewGuid())
    {
    }
}