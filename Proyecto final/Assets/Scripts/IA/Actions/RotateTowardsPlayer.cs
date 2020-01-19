using UnityEngine;

namespace BehaviorTree{

    class RotateTowardsPlayer : LeafNode{

        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public RotateTowardsPlayer(GameObject agent, ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            Debug.Log("Rotate towards PLAYER!");
            ArtificialIntelligenceInfo.SetSpaceshipRotation(Quaternion.LookRotation(ArtificialIntelligence.Target.transform.position - Agent.transform.position));
            return Status.BH_SUCCESS;
        }

    }
}

