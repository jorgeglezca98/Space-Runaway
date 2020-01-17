﻿using UnityEngine;

namespace BehaviorTree
{
    class StartCooldown : LeafNode
    {
        OverheatStats overheatData;

        public StartCooldown(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            overheatData.setIsCoolingDown(true);
            overheatData.setCooldownStartTime(Time.time);
            return Status.BH_SUCCESS;
        }
    }
}
