using UnityEngine;

namespace BehaviorTree
{
    class IsObjectToTheLeft : LeafNode
    {
        private int SecureDistance;
        private float HalfTheShipsHeight;
        private float HalfTheShipsLength;

        public IsObjectToTheLeft(GameObject agent, int SecureDistance, float HalfTheShipsHeight, float HalfTheShipsLength) : base(agent)
        {
            this.SecureDistance = SecureDistance;
            this.HalfTheShipsLength = HalfTheShipsLength;
            this.HalfTheShipsHeight = HalfTheShipsHeight;
        }

        public override Status Update()
        {
            bool objectLeft = Physics.BoxCast(Agent.transform.position, new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
               Agent.transform.TransformDirection(Vector3.left), Quaternion.identity, SecureDistance, ~(1 << 8) & ~(1 << 9));
            
            if (objectLeft)
                return Status.BH_SUCCESS;
            else
                return Status.BH_FAILURE;
        }
    }
}
