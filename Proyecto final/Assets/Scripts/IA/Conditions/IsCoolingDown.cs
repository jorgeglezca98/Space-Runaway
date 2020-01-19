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
                Debug.Log("Is cooling down");
                return Status.BH_SUCCESS;
            }
            Debug.Log("Is NOT cooling down");
            return Status.BH_FAILURE;
        }
    }
}
