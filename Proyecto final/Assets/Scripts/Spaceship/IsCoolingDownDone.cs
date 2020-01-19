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
            // TODO MAL! Tiene que calcular el tiempo simplemente, no vale con hacer el getcolingdown
            if (overheatData.getIsCoolingDown())
            {
                Debug.Log("Is STILL cooling down");
                return Status.BH_RUNNING;
            }
            Debug.Log("Is NOT STILL cooling down");
            return Status.BH_SUCCESS;
        }
    }
}
