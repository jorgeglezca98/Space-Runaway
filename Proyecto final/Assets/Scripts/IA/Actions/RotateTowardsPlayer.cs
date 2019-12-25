using UnityEngine;

namespace BehaviorTree{

    class RotateTowardsPlayer : LeafNode{

        public GameObject Player;


        public RotateTowardsPlayer(GameObject agent) : base(agent)
        {

        }

        public override Status Update()
        {
            Agent.transform.LookAt(Player.transform);
            return Status.BH_SUCCESS;
        }

    }
}

