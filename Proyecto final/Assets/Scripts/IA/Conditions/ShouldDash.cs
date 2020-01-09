using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    class ShouldDash : LeafNode
    {
        float actualHealth = PlayerStats.getHealth();
        float lastHealth;

        float diff = 20;

        public ShouldDash(GameObject agent) : base(agent)
        {
            lastHealth = actualHealth;
        }

        public override Status Update()
        {
            actualHealth = PlayerStats.getHealth();
            if (Mathf.Abs(lastHealth - actualHealth) >= 20)
            {
                lastHealth = actualHealth;
                return Status.BH_SUCCESS;
            }
            return Status.BH_FAILURE;
        }

    }
}