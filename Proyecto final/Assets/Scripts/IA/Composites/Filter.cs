using System.Collections.Generic;

namespace BehaviorTree
{

    class Filter : Sequence
    {
        public Filter() { }

        public Filter(Behavior b) : base(b) { }

        public Filter(List<Behavior> b) : base(b) { }

        public void AddCondition(Behavior condition)
        {
            children.Add(condition);
        }

        public void AddCondition(List<Behavior> conditions)
        {
            children.AddRange(conditions);
        }

        public void AddAction(Behavior action)
        {
            children.Add(action);
        }

        public void AddAction(List<Behavior> actions)
        {
            children.AddRange(actions);
        }
    }
}
