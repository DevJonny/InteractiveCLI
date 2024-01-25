using InteractiveCLI.Menus;

namespace SimpleApp.Menus;

public class TopLevelMenu() : Menu(true, true)
{
    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<SubMenu>( "Sub Menu", "A secondary menu with asynchronous actions");
    }
}