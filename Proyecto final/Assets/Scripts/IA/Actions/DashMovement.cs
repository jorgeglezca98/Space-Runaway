using UnityEngine;

namespace BehaviorTree
{
    public class DashMovement : LeafNode
    {
        private Vector3 rightDash;
        private Vector3 leftDash;
        private int secureDistance;
        private float halfTheShipsHeight;
        private float halfTheShipsLength;
        private int intensity;
        private System.Random random = new System.Random();

        public DashMovement(GameObject agent, int secureDistance, int intensity,
            float halfTheShipsHeight, float halfTheShipsLength) : base(agent)
        {
            this.intensity = intensity;
            this.secureDistance = secureDistance;
            this.halfTheShipsLength = halfTheShipsLength;
            this.halfTheShipsHeight = halfTheShipsHeight;
            rightDash = new Vector3(intensity, 0, 0);
            leftDash = new Vector3(intensity, 0, 0);
        }

        public override Status Update()
        {
            bool objectLeft = Physics.BoxCast(agent.transform.position, new Vector3(halfTheShipsLength, halfTheShipsHeight, 0),
                -agent.transform.right, agent.transform.rotation * Quaternion.Euler(0, 90, 0), secureDistance, ~(1 << 8) & ~(1 << 10));
            bool objectRight = Physics.BoxCast(agent.transform.position, new Vector3(halfTheShipsLength, halfTheShipsHeight, 0),
                agent.transform.right, agent.transform.rotation * Quaternion.Euler(0, 90, 0), secureDistance, ~(1 << 8) & ~(1 << 10));

            if (objectLeft && !objectRight)
            {
                agent.GetComponent<Rigidbody>().AddRelativeForce(rightDash, ForceMode.Impulse);
            }
            else if (!objectLeft && objectRight)
            {
                agent.GetComponent<Rigidbody>().AddRelativeForce(leftDash, ForceMode.Impulse);
            }
            else if (!objectLeft && !objectRight)
            {
                agent.GetComponent<Rigidbody>().AddRelativeForce(leftDash, ForceMode.Impulse);
            }
            else
            {
                return Status.BH_FAILURE;
            }

            return Status.BH_SUCCESS;
        }
    }
}
