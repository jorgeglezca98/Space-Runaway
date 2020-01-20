using UnityEngine;

namespace BehaviorTree
{

    class While : Decorator
    {
        public While(Behavior child) : base(child)
        {
        }

        public override Status Update()
        {
            while (true)
            {
                Status s = child.Tick();
                if (s == Status.BH_FAILURE) break;
                if (s != Status.BH_SUCCESS) return s;
            }
            return Status.BH_SUCCESS;
        }
    }
}
