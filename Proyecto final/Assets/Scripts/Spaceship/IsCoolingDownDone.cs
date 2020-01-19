using UnityEngine;
using System;

namespace BehaviorTree
{
    class IsCoolingDownDone : LeafNode
    {
        OverheatStats overheatData;

        public IsCoolingDownDone(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            if (Math.Abs(overheatData.getCooldownStartTime() - Time.time) <= overheatData.maxOverheatPenalizationTime)
            {
                //Debug.Log("Is STILL cooling down");
                return Status.BH_RUNNING;
            }
            // Debug.Log("Is NOT STILL cooling down");
            return Status.BH_SUCCESS;
        }
    }
}
