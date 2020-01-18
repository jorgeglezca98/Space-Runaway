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
            Children.Add(condition);
        }

        public void AddCondition(List<Behavior> conditions)
        {
            Children.AddRange(conditions);
        }

        public void AddAction(Behavior action)
        {
            Children.Add(action);
        }

        public void AddAction(List<Behavior> actions)
        {
            Children.AddRange(actions);
        }
    }
}
