using UnityEngine;

namespace BehaviorTree{

    class RotateTowardsPlayer : LeafNode{

        public RotateTowardsPlayer(GameObject agent) : base(agent)
        {

        }

        public override Status Update()
        {
            //Debug.Log("Rotate towards PLAYER!");
            Agent.transform.LookAt(ArtificialIntelligence.Target.transform);
            return Status.BH_SUCCESS;
        }

    }
}

