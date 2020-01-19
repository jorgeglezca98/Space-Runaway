using UnityEngine;

namespace BehaviorTree
{
    class ChangeFacingAsteroid : LeafNode
    {
        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public ChangeFacingAsteroid(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            Debug.Log("Changing facing asteroid");
            ArtificialIntelligenceInfo.SetPreviousAsteroidPosition(ArtificialIntelligenceInfo.GetCurrentAsteroidPosition());
            return Status.BH_SUCCESS;
        }
    }
}
