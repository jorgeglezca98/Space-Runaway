using UnityEngine;

namespace BehaviorTree
{
    class DashLeft : LeafNode
    {
        private Vector3 LeftDash;
        private int Intensity;

        public DashLeft(GameObject agent, int Intensity) : base(agent)
        {
            LeftDash = new Vector3(-Intensity, 0, 0);
        }

        public override Status Update()
        {
            Agent.GetComponent<Rigidbody>().AddRelativeForce(LeftDash, ForceMode.Impulse);
            return Status.BH_SUCCESS;
        }
    }
}
