using UnityEngine;

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
            if (overheatData.getIsCoolingDown())
            {
                return Status.BH_RUNNING;
            }
            return Status.BH_SUCCESS;
        }
    }
}
