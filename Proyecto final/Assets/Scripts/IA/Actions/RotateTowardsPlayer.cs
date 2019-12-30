using UnityEngine;

namespace BehaviorTree{

    class RotateTowardsPlayer : LeafNode{

        public RotateTowardsPlayer(GameObject agent) : base(agent)
        {

        }

        public override Status Update()
        {
            Agent.transform.LookAt(ArtificialIntelligence.Target.transform);
            return Status.BH_SUCCESS;
        }

    }
}

