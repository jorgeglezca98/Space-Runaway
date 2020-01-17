using UnityEngine;

namespace BehaviorTree
{
    class IsHealthLow : LeafNode
    {
        private float actualHealth;
        private float healthThreshold;

        public IsHealthLow(GameObject agent, DestructionController destructionController, float healthThreshold) : base(agent)
        {
            var stats = destructionController.Stats;
            actualHealth = stats.getHealth();
        }

        public override Status Update()
        {
            if (actualHealth <= healthThreshold)
                return Status.BH_SUCCESS;
            
            return Status.BH_FAILURE;
        }
    }
}
