using InteractiveCLI.Menus;
using SampleMenusAndActions.RepeatableActions;
using SampleMenusAndActions.SingleActions;

namespace SampleMenusAndActions.Menus;

public class SubMenu() : Menu(false, false)
{
    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<AsynchronousAction>("Asynchronous Action", "An asynchronous one time action")
            .AddMenuItem<AsynchronousRepeatableAction>("Asynchronous Repeatable Action", "An asynchronous repeatable action");
    }
}