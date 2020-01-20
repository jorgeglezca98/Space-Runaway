using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsOverheatGreaterThanZero : LeafNode
    {
        OverheatStats OverheatData;

        public IsOverheatGreaterThanZero(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            OverheatData = overheatData;
        }

        public override Status Update()
        {
            if (OverheatData.GetOverheat() > 0)
            {
                //Debug.Log("Overheat is over 0");
                return Status.BH_SUCCESS;
            }
            else
            {
                return Status.BH_FAILURE;
            }
        }

    }
}
