using UnityEngine;

namespace BehaviorTree
{
    internal class IsFacingNewAsteroid : LeafNode
    {
        private RotationInfo rotationInfo;

        public IsFacingNewAsteroid(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            if (rotationInfo.CurrentAsteroidPosition != rotationInfo.PreviousAsteroidPosition)
            {
                //Debug.Log("Facing new asteroid");
                return Status.BH_SUCCESS;
            }
            else
            {
                //Debug.Log("Facing last asteroid");
                return Status.BH_FAILURE;
            }
        }
    }
}
