using InteractiveCLI.Menus;
using SimpleApp.SingleActions;

namespace SimpleApp.Menus;

public class SubMenu() : Menu(false, false)
{
    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<NonMenuAction>("Asynchronous Action", "An asynchronous one time action");
    }
}