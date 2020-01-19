using UnityEngine;

namespace BehaviorTree
{
    class Yaw : LeafNode
    {
        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public Yaw(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            if (ArtificialIntelligenceInfo.GetCurrentDirection() == ArtificialIntelligenceInfo.Direction.Right)
            {
                Debug.Log("Yaw to RIGHT");
                ArtificialIntelligenceInfo.SetSpaceshipRotation(ArtificialIntelligenceInfo.GetSpaceshipRotation() * Quaternion.Euler(0, 1f, 0));
            }
            else
            {
                Debug.Log("Yaw to LEFT");
                ArtificialIntelligenceInfo.SetSpaceshipRotation(ArtificialIntelligenceInfo.GetSpaceshipRotation() * Quaternion.Euler(0, -1f, 0));
            }

            return Status.BH_SUCCESS;
        }
    }
}
