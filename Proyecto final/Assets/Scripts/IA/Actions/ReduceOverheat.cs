using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class ReduceOverheat : LeafNode
    {
        float OverheatDecrement;
        OverheatStats OverheatData;

        public ReduceOverheat(GameObject agent, float overheatDecrement, 
                        OverheatStats overheatData) : base(agent)
        {
            OverheatDecrement = overheatDecrement;
            OverheatData = overheatData;
        }

        public override Status Update()
        {
            OverheatData.setOverheat(OverheatData.getOverheat() - OverheatDecrement);
            return Status.BH_SUCCESS;
        }

    }
}
