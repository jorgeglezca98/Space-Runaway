using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsTargetVisible : LeafNode
    {
        public IsTargetVisible(GameObject agent) : base(agent)
        {
        }

        public override Status Update()
        {
            RaycastHit hit;

            bool collidesWithSomething = Physics.Raycast(agent.transform.position,
                ArtificialIntelligence.target.transform.position - agent.transform.position, out hit, Mathf.Infinity, ~(1 << 8));
            bool targetIsHit = GameObject.ReferenceEquals(ArtificialIntelligence.target, hit.transform.gameObject);

            if (collidesWithSomething && targetIsHit)
            {
                //Debug.Log("Target is visible");
                return Status.BH_SUCCESS;
            }
            else
            {
                //Debug.Log("Target is NOT visible");
                return Status.BH_FAILURE;
            }
        }
    }
}