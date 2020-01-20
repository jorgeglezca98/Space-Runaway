using UnityEngine;

namespace BehaviorTree
{
    public class MoveBack : LeafNode
    {
        private int velocity;

        public MoveBack(GameObject agent, int velocity) : base(agent)
        {
            this.velocity = velocity;
        }

        public override Status Update()
        {
            //Debug.Log("Moving back!");
            agent.transform.position -= new Vector3(0, 0, Time.deltaTime * velocity * agent.transform.forward.z);
            return Status.BH_SUCCESS;
        }
    }
}
