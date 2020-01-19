using UnityEngine;

namespace BehaviorTree
{
    class DashRight : LeafNode
    {
        private Vector3 RightDash;
        private int Intensity;

        System.Random random = new System.Random();

        public DashRight(GameObject agent, int Intensity) : base(agent)
        {
            RightDash = new Vector3(Intensity, 0, 0);
        }

        public override Status Update()
        {
            //Debug.Log("MOVE RIGHT");
            Agent.GetComponent<Rigidbody>().AddRelativeForce(RightDash, ForceMode.Impulse);
            return Status.BH_SUCCESS;
        }
    }
}

