namespace BehaviorTree
{
    public abstract class Decorator : Behavior
    {
        protected Behavior child;

        protected Decorator(Behavior child)
        {
            this.child = child;
        }
    }
}
