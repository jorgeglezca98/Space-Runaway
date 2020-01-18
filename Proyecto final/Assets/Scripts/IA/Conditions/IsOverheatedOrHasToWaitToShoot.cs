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

            if (overheat >= overheatUpperThreshold)
            {
                waitToShoot = true;
            }
            else if (overheat <= overheatLowerThreshold)
            {
                waitToShoot = false;
                return Status.BH_SUCCESS;
            }
            else if (!waitToShoot)
            {
                return Status.BH_SUCCESS;
            }

            return Status.BH_FAILURE;
        }
    }
}
