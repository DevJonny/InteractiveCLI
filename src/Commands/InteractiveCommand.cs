namespace InteractiveCLI.Commands;

public class InteractiveCommand : IRunnable
{
    private readonly Runner _runner;

    public InteractiveCommand(Runner runner)
    {
        _runner = runner;
    }

    public Task Run()
    {
        throw new System.NotImplementedException();
    }
}