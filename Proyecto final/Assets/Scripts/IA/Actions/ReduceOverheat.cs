using UnityEngine;

namespace BehaviorTree
{
    internal class ReduceOverheat : LeafNode
    {
        private OverheatStats overheatData;

        public ReduceOverheat(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            //Debug.Log("Reducing overheat");
            overheatData.SetOverheat(overheatData.GetOverheat() - overheatData.overheatDecrement);
            return Status.BH_SUCCESS;
        }
    }
}
