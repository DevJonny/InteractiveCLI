using InteractiveCLI.Actions;

namespace InteractiveCLI.Menus;

public class MenuBuilder
{
    internal List<(Type ActionType, string Name, string Description)> MenuItems { get; } = new();

    public MenuBuilder AddMenuItem<T>(string name, string description) where T : IAmAnAction
    {
        MenuItems.Add((typeof(T), name, description));

        return this;
    }

    public MenuBuilder AddMenuItem<T>(string name, string description, Func<bool> inclusionPredicate) where T : IAmAnAction
    {
        return inclusionPredicate() ? AddMenuItem<T>(name, description) : this;
    }

    public MenuBuilder AddMenuItem<T>(string name, string description, bool includeIf) where T : IAmAnAction
    {
        return includeIf ? AddMenuItem<T>(name, description) : this;
    }
}