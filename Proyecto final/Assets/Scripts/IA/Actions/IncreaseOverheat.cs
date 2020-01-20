using UnityEngine;

namespace BehaviorTree
{
    public class IncreaseOverheat : LeafNode
    {
        private OverheatStats overheatData;

        public IncreaseOverheat(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            overheatData.SetOverheat(overheatData.GetOverheat() + overheatData.overheatIncrement);
            //Debug.Log("Increasing overheat " + overheatData.getMaxOverheat());
            return Status.BH_SUCCESS;
        }
    }
}
