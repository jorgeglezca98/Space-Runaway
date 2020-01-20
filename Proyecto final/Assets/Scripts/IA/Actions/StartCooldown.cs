using UnityEngine;

namespace BehaviorTree
{
    internal class StartCooldown : LeafNode
    {
        private OverheatStats overheatData;

        public StartCooldown(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            //Debug.Log("Start cooling down");
            overheatData.SetIsCoolingDown(true);
            overheatData.SetCooldownStartTime(Time.time);
            return Status.BH_SUCCESS;
        }
    }
}
