using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsTargetInRange : LeafNode
    {

        public int ShotMaxDistance;
        public int ShotMinDistance;
        public int RangeSize;

        public IsTargetInRange(GameObject agent, int shotMaxDistance, 
                                    int rangeSize, int shotMinDistance) : base(agent)
        {
            ShotMaxDistance = shotMaxDistance;
            RangeSize = rangeSize;
            ShotMinDistance = (shotMinDistance < 10) ? 10 : shotMinDistance;
        }

        public override Status Update()
        {
            bool isInRange = Physics.BoxCast(Agent.transform.position, new Vector3(RangeSize, RangeSize, ShotMinDistance),
                Agent.transform.TransformDirection(Vector3.forward), Quaternion.identity, ShotMaxDistance, (1 << 9));

            return isInRange ? Status.BH_SUCCESS : Status.BH_FAILURE;
        }
    }
}