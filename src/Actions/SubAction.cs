namespace InteractiveCLI.Actions;

public class SubAction : ActionBase
{
    protected override IDictionary<string, (Func<Task<bool>> Action, string Description)> AvailableActions
        => new Dictionary<string, (Func<Task<bool>> Action, string Description)>
        {
            {"Print Something", (PrintSomething, "Just something to print")}
        };
        
    public override async Task Run() =>
        await SelectOption(
            BeforeOptions, 
            input => AvailableActions.ContainsKey(input) ? AvailableActions[input].Action : null);

    private static async Task BeforeOptions()
    {
        await Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Foo");
        });
    }

    private async Task<bool> PrintSomething()
    {
        Console.WriteLine("Print some text about how sub-actions can be used including a REST example.");
        return await Task.FromResult(true);
    }
}