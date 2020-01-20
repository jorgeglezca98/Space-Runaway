using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsTargetVisibleInFront : LeafNode
    {
        public IsTargetVisibleInFront(GameObject agent) : base(agent)
        {
        }

        public override Status Update()
        {
            try
            {
                RaycastHit hit;

                bool collidesWithSomething = Physics.Raycast(agent.transform.position, agent.transform.forward,
                                                             out hit, Mathf.Infinity, ~(1 << 8));

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
            catch(Exception e)
            {
                return Status.BH_FAILURE;
            }
        }
    }
}