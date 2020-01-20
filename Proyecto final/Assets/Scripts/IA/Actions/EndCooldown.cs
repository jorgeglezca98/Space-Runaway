using UnityEngine;

namespace BehaviorTree
{
    public class EndCooldown : LeafNode
    {
        private OverheatStats overheatData;

        public EndCooldown(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            //Debug.Log("End Cooldown");
            overheatData.SetIsCoolingDown(false);
            return Status.BH_SUCCESS;
        }
    }
}
