using UnityEngine;

namespace BehaviorTree
{
    class ChangeCurrentYawDirection : LeafNode
    {
        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public ChangeCurrentYawDirection(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            ArtificialIntelligenceInfo.SetCurrentDirection(DirectionToTarget());
            return Status.BH_SUCCESS;
        }

        ArtificialIntelligenceInfo.Direction DirectionToTarget()
        {
            float angle1 = Quaternion.Angle(Agent.transform.rotation * Quaternion.Euler(0f, 45f, 0f), ArtificialIntelligence.Target.transform.rotation) % 360;
            float angle2 = Quaternion.Angle(Agent.transform.rotation * Quaternion.Euler(0f, -45f, 0f), ArtificialIntelligence.Target.transform.rotation) % 360;
            if (angle1 < angle2)
            {
                return ArtificialIntelligenceInfo.Direction.Right;
            }
            else return ArtificialIntelligenceInfo.Direction.Left;
        }

    }


}
