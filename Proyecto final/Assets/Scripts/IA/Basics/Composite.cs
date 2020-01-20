using System.Collections.Generic;

namespace BehaviorTree
{
    public abstract class Composite : Behavior
    {
        protected List<Behavior> children = new List<Behavior>();

        protected Composite() { }

        protected Composite(Behavior behavior)
        {
            children.Add(behavior);
        }

        protected Composite(List<Behavior> behaviors)
        {
            children.AddRange(behaviors);
        }

        public void AddChild(Behavior b)
        {
            children.Add(b);
        }

        public void AddChild(List<Behavior> behaviorList)
        {
            children.AddRange(behaviorList);
        }

        public void RemoveChild(Behavior b)
        {
            children.Remove(b);
        }

        public void ClearChildren()
        {
            children.Clear();
        }
    }
}
