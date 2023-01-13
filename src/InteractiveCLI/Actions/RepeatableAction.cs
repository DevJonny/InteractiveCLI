using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public class RepeatableAction : Command
{
    public RepeatableAction() : base(Guid.NewGuid())
    {
    }
}