using UnityEngine;

namespace BehaviorTree
{
    class ApplyRotation : LeafNode
    {
        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public ApplyRotation(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            Agent.transform.rotation = ArtificialIntelligenceInfo.GetSpaceshipRotation();
            return Status.BH_SUCCESS;
        }
    }
}
