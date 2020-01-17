using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsOverheated : LeafNode
    {
        OverheatStats OverheatData;

        public IsOverheated(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            OverheatData = overheatData;
        }

        public override Status Update()
        {
            if(OverheatData.getOverheat() >= OverheatData.getMaxOverheat()) {
                return Status.BH_SUCCESS;
            } else {
                return Status.BH_FAILURE;
            }
        }

    }
}
