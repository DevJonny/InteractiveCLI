using InteractiveCLI.Menus;
using Paramore.Brighter;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;

namespace SampleMenusAndActions.Menus;

public class SubMenuHandler : MenuHandlerBase<SubMenu>
{
    public SubMenuHandler(IAmACommandProcessor commandProcessor) : base(commandProcessor, true)
    {
    }

    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<AsynchronousAction>("Asynchronous Action", "An asynchronous one time action")
            .AddMenuItem<AsynchronousRepeatableAction>("Asynchronous Repeatable Action", "An asynchronous repeatable action");
    }
}

public class SubMenu : Menu
{
}