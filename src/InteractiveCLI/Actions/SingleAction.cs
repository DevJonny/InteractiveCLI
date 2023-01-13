using Paramore.Brighter;

namespace InteractiveCLI.Actions;

public class SingleAction : Command
{
    public SingleAction() : base(Guid.NewGuid())
    {
    }
}