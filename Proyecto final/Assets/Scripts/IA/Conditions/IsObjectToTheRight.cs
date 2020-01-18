using UnityEngine;

namespace BehaviorTree
{
    class IsObjectToTheRight : LeafNode
    {
        private int SecureDistance;
        private float HalfTheShipsHeight;
        private float HalfTheShipsLength;

        public IsObjectToTheRight(GameObject agent, int SecureDistance, float HalfTheShipsHeight, float HalfTheShipsLength) : base(agent)
        {
            this.SecureDistance = SecureDistance;
            this.HalfTheShipsLength = HalfTheShipsLength;
            this.HalfTheShipsHeight = HalfTheShipsHeight;
        }

        public override Status Update()
        {
            bool objectRight = Physics.BoxCast(Agent.transform.position, new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
                Agent.transform.TransformDirection(Vector3.right), Quaternion.identity, SecureDistance, ~(1 << 8) & ~(1 << 9));

            if (objectRight)
                return Status.BH_SUCCESS;
            else
                return Status.BH_FAILURE;
        }
    }
}
