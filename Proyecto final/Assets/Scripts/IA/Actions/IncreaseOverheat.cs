using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    class IncreaseOverheat : LeafNode
    {
        float OverheatIncrement;
        OverheatStats OverheatData;

        public IncreaseOverheat(GameObject agent, float overheatIncrement,
                        OverheatStats overheatData) : base(agent)
        {
            OverheatIncrement = overheatIncrement;
            OverheatData = overheatData;
        }

        public override Status Update()
        {
            OverheatData.setOverheat(OverheatData.getOverheat() + OverheatIncrement);
            return Status.BH_SUCCESS;
        }
    }
}
