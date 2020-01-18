using System.Collections.Generic;

namespace BehaviorTree
{

    abstract class Composite : Behavior
    {
        protected List<Behavior> Children = new List<Behavior>();

        protected Composite() { }

        protected Composite(Behavior behavior)
        {
            Children.Add(behavior);
        }

        protected Composite(List<Behavior> behaviors)
        {
            Children.AddRange(behaviors);
        }

        public void AddChild(Behavior b)
        {
            Children.Add(b);
        }

        public void AddChild(List<Behavior> behaviorList)
        {
            Children.AddRange(behaviorList);
        }

        public void RemoveChild(Behavior b)
        {
            Children.Remove(b);
        }

        public void ClearChildren()
        {
            Children.Clear();
        }
    }
}
