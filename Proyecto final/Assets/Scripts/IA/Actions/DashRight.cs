using UnityEngine;

namespace BehaviorTree
{
    public class DashRight : LeafNode
    {
        private Vector3 rightDash;
        private System.Random random = new System.Random();

        public DashRight(GameObject agent, int intensity) : base(agent)
        {
            rightDash = new Vector3(intensity, 0, 0);
        }

        public override Status Update()
        {
            //Debug.Log("MOVE RIGHT");
            agent.GetComponent<Rigidbody>().AddRelativeForce(rightDash, ForceMode.Impulse);
            return Status.BH_SUCCESS;
        }
    }
}
