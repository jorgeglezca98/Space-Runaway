using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class IsTargetVisible : LeafNode
    {

        public int ShotMaxDistance;

        public IsTargetVisible(GameObject agent, int shotMaxDistance) : base(agent)
        {
            ShotMaxDistance = shotMaxDistance;
        }

        public override Status Update()
        {
            RaycastHit hit;

            bool collidesWithSomething = Physics.Raycast(Agent.transform.position,
                ArtificialIntelligence.Target.transform.position - Agent.transform.position, out hit, ShotMaxDistance, ~(1 << 8));
            bool targetIsHit = GameObject.ReferenceEquals(ArtificialIntelligence.Target, hit.transform.gameObject);

            if (collidesWithSomething && targetIsHit)
            {
                Debug.Log("Target is visible");
                return Status.BH_SUCCESS;
            }
            else
            {
                Debug.Log("Target is NOT visible");
                return Status.BH_FAILURE;
            }
        }
    }
}