using UnityEngine;

namespace BehaviorTree
{
    public class Yaw : LeafNode
    {
        private RotationInfo rotationInfo;

        public Yaw(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            if (rotationInfo.CurrentDirection == RotationInfo.Direction.Right)
            {
                //Debug.Log("Yaw to RIGHT");
                rotationInfo.SpaceshipRotation = rotationInfo.SpaceshipRotation * Quaternion.Euler(0, 1f, 0);
            }
            else
            {
                //Debug.Log("Yaw to LEFT");
                rotationInfo.SpaceshipRotation = rotationInfo.SpaceshipRotation * Quaternion.Euler(0, -1f, 0);
            }

            return Status.BH_SUCCESS;
        }
    }
}
