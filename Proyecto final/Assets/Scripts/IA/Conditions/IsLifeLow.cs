using UnityEngine;

namespace BehaviorTree
{
    class IsHealthLow : LeafNode
    {
        private LifeStats stats;
        private float actualHealth;
        private float healthThreshold;

        public IsHealthLow(GameObject agent, DestructionController destructionController, float healthThreshold) : base(agent)
        {
            this.healthThreshold = healthThreshold;
            stats = destructionController.Stats;
            actualHealth = stats.GetHealth();
        }

        public override Status Update()
        {
            actualHealth = stats.GetHealth();
            //Debug.Log("Actual: " + actualHealth + ", Healt Threshold: " + healthThreshold + ", " + stats.getMaxHealth());

            if (actualHealth <= healthThreshold)
            {
                //Debug.Log("Health is LOW");
                return Status.BH_SUCCESS;
            }
            
            return Status.BH_FAILURE;
        }
    }
}
