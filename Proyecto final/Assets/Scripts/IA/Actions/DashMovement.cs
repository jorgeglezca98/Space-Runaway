using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    class DashMovement : LeafNode
    {
        Vector3 rightDash = new Vector3(Time.deltaTime * 350, 0, 0);
        Vector3 leftDash = new Vector3(-Time.deltaTime * 350, 0, 0);

        public int ShotMaxDistance = 10;
        public int ShotMinDistance = 0;
        public int RangeSize = 10;

        System.Random random = new System.Random();

        public DashMovement(GameObject agent) : base(agent)
        { }

        public override Status Update()
        {
            RaycastHit hit;

            bool objectLeft = Physics.BoxCast(Agent.transform.position, new Vector3(RangeSize, RangeSize, ShotMinDistance),
                Agent.transform.TransformDirection(Vector3.left), out hit, Quaternion.identity, ShotMaxDistance, ~(1 << 8));
            bool objectRight = Physics.BoxCast(Agent.transform.position, new Vector3(RangeSize, RangeSize, ShotMinDistance),
                Agent.transform.TransformDirection(Vector3.right), out hit, Quaternion.identity, ShotMaxDistance, ~(1 << 8));

            if (objectLeft && !objectRight)
            {
                Agent.GetComponent<Rigidbody>().AddRelativeForce(rightDash, ForceMode.Impulse);
            }
            else if (!objectLeft && objectRight)
            {
                Agent.GetComponent<Rigidbody>().AddRelativeForce(leftDash, ForceMode.Impulse);
            }
            else if (!objectLeft && !objectRight)
            {
                switch(random.Next(2))
                {
                    case 0:
                        Agent.GetComponent<Rigidbody>().AddRelativeForce(leftDash, ForceMode.Impulse);
                        break;
                    case 1:
                        Agent.GetComponent<Rigidbody>().AddRelativeForce(rightDash, ForceMode.Impulse);
                        break;
                }
            }
           
            return Status.BH_SUCCESS;
        }
    }
}
