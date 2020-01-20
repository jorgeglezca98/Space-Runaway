using UnityEngine;

namespace BehaviorTree
{
    public class DashLeft : LeafNode
    {
        private Vector3 leftDash;

        public DashLeft(GameObject agent, int intensity) : base(agent)
        {
            leftDash = new Vector3(-intensity, 0, 0);
        }

        public override Status Update()
        {
            //Debug.Log("MOVE LEFT");
            agent.GetComponent<Rigidbody>().AddRelativeForce(leftDash, ForceMode.Impulse);
            return Status.BH_SUCCESS;
        }
    }
}
