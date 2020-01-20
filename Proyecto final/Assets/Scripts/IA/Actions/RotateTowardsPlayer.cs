using UnityEngine;

namespace BehaviorTree
{
    public class RotateTowardsPlayer : LeafNode
    {
        private RotationInfo rotationInfo;

        public RotateTowardsPlayer(GameObject agent, RotationInfo rotationInfo) : base(agent)
        {
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            //Debug.Log("Rotate towards PLAYER!");
            rotationInfo.SpaceshipRotation = Quaternion.LookRotation(ArtificialIntelligence.target.transform.position - agent.transform.position);
            return Status.BH_SUCCESS;
        }
    }
}
