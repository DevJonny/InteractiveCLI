namespace InteractiveCLI.Menus;

public class MenuBuilder
{
    internal List<(Type CommandType, string Name, string Description)> MenuItems { get; } = new();

    public MenuBuilder AddMenuItem<T>(string name, string description) where T : InteractiveCLI.Actions.Action
    {
        MenuItems.Add((typeof(T), name, description));

        return this;
    }
}