using UnityEngine;

namespace BehaviorTree
{
    class EndCooldown : LeafNode
    {
        OverheatStats overheatData;

        public EndCooldown(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            //Debug.Log("End Cooldown");
            overheatData.setIsCoolingDown(false);
            return Status.BH_SUCCESS;
        }
    }
}
