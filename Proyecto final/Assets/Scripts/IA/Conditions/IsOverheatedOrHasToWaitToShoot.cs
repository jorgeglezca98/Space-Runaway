using UnityEngine;

namespace BehaviorTree
{
    class IsOverheatedOrHasToWaitToShoot : LeafNode
    {
        private float overheatUpperThreshold;
        private float overheatLowerThreshold;
        private OverheatStats overheatData;

        bool waitToShoot = false;

        public IsOverheatedOrHasToWaitToShoot(GameObject agent, OverheatStats overheatData, float overheatUpperThreshold, float overheatLowerThreshold) : base(agent)
        {
            this.overheatData = overheatData;
            this.overheatUpperThreshold = overheatUpperThreshold;
            this.overheatLowerThreshold = overheatLowerThreshold;
        }

        public override Status Update()
        {
            float overheat = overheatData.getOverheat();

            Debug.Log("ACTUAL OVERHEAT : " + overheat + ", Upper: " + overheatUpperThreshold + ", MAX" + overheatData.getMaxOverheat());

            if (overheat >= overheatUpperThreshold)
            {
                Debug.Log("Is over threshold overheat! It has to wait to shoot");
                waitToShoot = true;
            }
            else if (overheat <= overheatLowerThreshold)
            {
                Debug.Log("Lower overheat threshold reached, no longer has to wait to shoot");
                waitToShoot = false;
                return Status.BH_FAILURE;
            }
            else if (!waitToShoot)
            {
                Debug.Log("Doesn't have to wait to shoot. Do it");
                return Status.BH_FAILURE;
            }

            Debug.Log("Can't shoot yet. It hasn't reached lower limit");
            return Status.BH_SUCCESS;
        }
    }
}
