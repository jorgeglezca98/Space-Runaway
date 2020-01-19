using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    class IncreaseOverheat : LeafNode
    {
        OverheatStats overheatData;

        public IncreaseOverheat(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            overheatData.setOverheat(overheatData.getOverheat() + overheatData.overheatIncrement);
            //Debug.Log("Increasing overheat " + overheatData.getMaxOverheat());
            return Status.BH_SUCCESS;
        }
    }
}
