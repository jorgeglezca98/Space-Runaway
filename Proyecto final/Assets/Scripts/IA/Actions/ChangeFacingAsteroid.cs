using UnityEngine;

namespace BehaviorTree
{
    public class ChangeFacingAsteroid : LeafNode
    {
        private RotationInfo rotationInfo;

        public ChangeFacingAsteroid(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            rotationInfo.PreviousAsteroidPosition = rotationInfo.CurrentAsteroidPosition;
            return Status.BH_SUCCESS;
        }
    }
}
