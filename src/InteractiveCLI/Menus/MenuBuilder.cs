namespace InteractiveCLI.Menus;

public class MenuBuilder
{
    internal List<(Type CommandType, string Name, string Description)> MenuItems { get; } = new();

    public MenuBuilder AddMenuItem<T>(string name, string description) where T : InteractiveCLI.Actions.Action
    {
        MenuItems.Add((typeof(T), name, description));

        return this;
    }

    public MenuBuilder AddMenuItem<T>(string name, string description, Func<bool> inclusionPredicate) where T : InteractiveCLI.Actions.Action
    {
        return inclusionPredicate() ? AddMenuItem<T>(name, description) : this;
    }

    public MenuBuilder AddMenuItem<T>(string name, string description, bool includeIf) where T : InteractiveCLI.Actions.Action
    {
        return includeIf ? AddMenuItem<T>(name, description) : this;
    }
}