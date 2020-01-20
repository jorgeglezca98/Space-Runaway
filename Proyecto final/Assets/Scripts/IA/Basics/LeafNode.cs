using UnityEngine;

namespace BehaviorTree
{
    public abstract class LeafNode : Behavior
    {
        protected GameObject agent;

        protected LeafNode(GameObject agent)
        {
            this.agent = agent;
        }
    }
}
