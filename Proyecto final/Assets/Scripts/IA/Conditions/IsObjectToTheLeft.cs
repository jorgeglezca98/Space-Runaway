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
              -Agent.transform.right, Agent.transform.rotation * Quaternion.Euler(0, 90, 0), SecureDistance, ~(1 << 8) & ~(1 << 10));

            Debug.Log("Entering IS OBJECT LEFT");
            if (objectLeft)
            {
                Debug.Log("Yes, there is object at left");
                return Status.BH_SUCCESS;
            }
            else
            {
                Debug.Log("NO, there isn't object at left");
                return Status.BH_FAILURE;
            }
        }
    }
}
