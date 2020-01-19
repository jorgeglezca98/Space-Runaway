using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{

    class IsFacingNewAsteroid : LeafNode
    {

        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public IsFacingNewAsteroid(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            if (ArtificialIntelligenceInfo.GetCurrentAsteroidPosition() != ArtificialIntelligenceInfo.GetPreviousAsteroidPosition())
            {
                Debug.Log("New asteroid");
                return Status.BH_SUCCESS;
            }
            else
            {
                return Status.BH_FAILURE;
            }
        }
    }
}
