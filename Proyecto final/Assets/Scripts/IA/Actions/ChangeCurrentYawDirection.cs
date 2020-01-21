using UnityEngine;

namespace BehaviorTree
{
    public class ChangeCurrentYawDirection : LeafNode
    {
        private RotationInfo rotationInfo;

        public ChangeCurrentYawDirection(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            rotationInfo.CurrentDirection = DirectionToTarget();
            return Status.BH_SUCCESS;
        }

        private RotationInfo.Direction DirectionToTarget()
        {
            float angle1 = Quaternion.Angle(rotationInfo.SpaceshipRotation * Quaternion.Euler(0f, 45f, 0f), ArtificialIntelligence.target.transform.rotation) % 360;
            float angle2 = Quaternion.Angle(rotationInfo.SpaceshipRotation * Quaternion.Euler(0f, -45f, 0f), ArtificialIntelligence.target.transform.rotation) % 360;
            if (angle1 < angle2)
            {
                //Debug.Log("change yaw to right");
                return RotationInfo.Direction.Right;
            }
            else
            {
                //Debug.Log("change yaw to left");
                return RotationInfo.Direction.Left;
            }
        }

    }


}
