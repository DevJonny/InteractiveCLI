using Paramore.Brighter;

namespace InteractiveCLI.SingleActions;

public abstract class SingleActionBase<T> : RequestHandler<T> where T : Command
{
    public override T Handle(T command)
    {
        Action();
        
        return base.Handle(command);
    }

    protected abstract void Action();
}