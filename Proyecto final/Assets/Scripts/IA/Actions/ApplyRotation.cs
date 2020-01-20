using UnityEngine;

namespace BehaviorTree
{
    public class ApplyRotation : LeafNode
    {
        private RotationInfo rotationInfo;

        public ApplyRotation(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            agent.transform.rotation = rotationInfo.SpaceshipRotation;
            return Status.BH_SUCCESS;
        }
    }
}
