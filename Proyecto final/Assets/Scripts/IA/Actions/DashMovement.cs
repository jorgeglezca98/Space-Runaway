using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    class DashMovement : LeafNode
    {
        private Vector3 RightDash;
        private Vector3 LeftDash;
        private int SecureDistance;
        private float HalfTheShipsHeight;
        private float HalfTheShipsLength;
        private int Intensity;

        System.Random random = new System.Random();

        public DashMovement(GameObject agent, int SecureDistance, int Intensity,
            float HalfTheShipsHeight, float HalfTheShipsLength) : base(agent)
        {
            this.Intensity = Intensity;
            this.SecureDistance = SecureDistance;
            this.HalfTheShipsLength = HalfTheShipsLength;
            this.HalfTheShipsHeight = HalfTheShipsHeight;
            RightDash = new Vector3(Intensity, 0, 0);
            LeftDash = new Vector3(Intensity, 0, 0);
        }

        public override Status Update()
        {
            bool objectLeft = Physics.BoxCast(Agent.transform.position, new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
                Agent.transform.TransformDirection(Vector3.left), Quaternion.identity, SecureDistance, ~(1 << 8) & ~(1 << 9));
            bool objectRight = Physics.BoxCast(Agent.transform.position, new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
                Agent.transform.TransformDirection(Vector3.right), Quaternion.identity, SecureDistance, ~(1 << 8) & ~(1 << 9));

            if (objectLeft && !objectRight)
            {
                Agent.GetComponent<Rigidbody>().AddRelativeForce(RightDash, ForceMode.Impulse);
            }
            else if (!objectLeft && objectRight)
            {
                Agent.GetComponent<Rigidbody>().AddRelativeForce(LeftDash, ForceMode.Impulse);
            }
            else if (!objectLeft && !objectRight)
            {
                Agent.GetComponent<Rigidbody>().AddRelativeForce(LeftDash, ForceMode.Impulse);
            }
            else return Status.BH_FAILURE;

            return Status.BH_SUCCESS;
        }
    }
}
