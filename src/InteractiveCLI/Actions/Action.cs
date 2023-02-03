using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public class Action : Command
{
    public Action() : base(Guid.NewGuid())
    {
    }
}