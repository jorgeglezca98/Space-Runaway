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
            Debug.Log("Is Object RIGHT?");
            bool objectRight = Physics.BoxCast(Agent.transform.position, new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
                Agent.transform.right, Agent.transform.rotation * Quaternion.Euler(0, 90, 0), SecureDistance, ~(1 << 8) & ~(1 << 10));

            if (objectRight)
            {
                Debug.Log("YES! THERE IS OBJECT RIGHT");
                return Status.BH_SUCCESS;
            }
            else
            {
                Debug.Log("No, there isn't object to the right");
                return Status.BH_FAILURE;
            }
        }
    }
}
