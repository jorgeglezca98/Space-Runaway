using UnityEngine;

namespace BehaviorTree
{
    class IsCoolingDown : LeafNode
    {
        OverheatStats overheatData;

        public IsCoolingDown(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            if (overheatData.getIsCoolingDown())
            {
                return Status.BH_SUCCESS;
            }
            return Status.BH_FAILURE;
        }
    }
}
