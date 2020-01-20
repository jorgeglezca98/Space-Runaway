namespace BehaviorTree
{
    public class BehaviorTree
    {
        protected Behavior Root { get; set; }

        public BehaviorTree(Behavior initialNode)
        {
            Root = initialNode;
        }

        public void Tick()
        {
            Root.Tick();
        }
    }
}
