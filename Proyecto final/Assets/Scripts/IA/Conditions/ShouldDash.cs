using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    class ShouldDash : LeafNode
    {
        LifeStats stats;
        float actualHealth;
        float lastHealth;

        float diff = 20f;
        private GameObject gameObject;
        private DestructionController destructionController;

        public ShouldDash(GameObject agent, DestructionController destructionController) : base(agent)
        {
            stats = destructionController.Stats;
            actualHealth = stats.getHealth();
            lastHealth = actualHealth;
        }

        public override Status Update()
        {
            actualHealth = stats.getHealth();
            if (Math.Abs(lastHealth - actualHealth) >= diff)
            {
                lastHealth = actualHealth;
                return Status.BH_SUCCESS;
            }
            return Status.BH_FAILURE;
        }
    }
}
