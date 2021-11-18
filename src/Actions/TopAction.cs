namespace InteractiveCLI.Actions;

public class TopAction : ActionBase
{
    protected override IDictionary<string, (Func<Task<bool>> Action, string Description)> AvailableActions =>
        new Dictionary<string, (Func<Task<bool>> Action, string Description)>
        {
            {"Action", (Action, "This is an action at the current level")},
            {"Sub-Action", (SubAction, "This is an action at a level down")}
        };
        
    public override async Task Run()
    {
        await SelectOption(
            null, 
            input => AvailableActions.ContainsKey(input) ? AvailableActions[input].Action : null, 
            null);
    }

    private Task<bool> Action()
    {
        return Task.FromResult(false);
    }

    private Task<bool> SubAction()
    {
        return Task.FromResult(false);
    }
}