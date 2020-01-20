using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{

    class AreAsteroidsInFront : LeafNode
    {
        Vector3 BoxcastDimension;
        float LookForCollisionDistance;
        ArtificialIntelligenceInfo ArtificialIntelligenceInfo;

        public AreAsteroidsInFront(GameObject agent, float lookForCollisionDistance, float shipsWingspan, float halfTheShipsLength, float halfTheShipsHeight,
                                    ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            BoxcastDimension = new Vector3(shipsWingspan, halfTheShipsHeight, halfTheShipsLength);
            LookForCollisionDistance = lookForCollisionDistance;
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            RaycastHit HittedObject;
            // bool ThereIsCollision = Physics.BoxCast(Agent.transform.position, BoxcastDimension, Agent.transform.forward,
            //     out HittedObject, Agent.transform.rotation, LookForCollisionDistance);
            bool ThereIsCollision = Physics.BoxCast(Agent.transform.position - BoxcastDimension.x * Agent.transform.forward, BoxcastDimension, Agent.transform.forward,
                out HittedObject, Agent.transform.rotation, LookForCollisionDistance, ~(1 << 8) & ~(1 << 10));
            if (ThereIsCollision)
            {
                if (HittedObject.collider.tag == "asteroid")
                {
                    Debug.Log("There is AN ASTEROID in FRONT!");
                    ArtificialIntelligenceInfo.SetCurrentAsteroidPosition(HittedObject.transform.position);
                    return Status.BH_SUCCESS;
                }
                else
                {
                    Debug.Log("There is something in front that is NOT an ASTEROID!");
                    return Status.BH_FAILURE;
                }
            }
            else
            {
                Debug.Log("There is NOTHING in FRONT!");
                return Status.BH_FAILURE;
            }
        }
    }
}
