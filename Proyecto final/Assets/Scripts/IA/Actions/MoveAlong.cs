using UnityEngine;

namespace BehaviorTree
{
    public class MoveForward : LeafNode
    {
        private int velocity;

        public MoveForward(GameObject agent, int velocity) : base(agent)
        {
            this.velocity = velocity;
        }

        public override Status Update()
        {
            //Debug.Log("MOVE FORWARD!!");
            agent.transform.position += agent.transform.forward * Time.deltaTime * velocity;
            return Status.BH_SUCCESS;
        }
    }
}
