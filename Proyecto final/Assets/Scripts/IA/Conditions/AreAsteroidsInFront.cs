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
        float HalfTheShipsLength;

        public AreAsteroidsInFront(GameObject agent, float lookForCollisionDistance, float shipsWingspan, float halfTheShipsLength, float halfTheShipsHeight,
                                    ArtificialIntelligenceInfo artificialIntelligenceInfo) : base(agent)
        {
            BoxcastDimension = new Vector3(shipsWingspan*1.5f, halfTheShipsHeight*1.5f, 0);
            HalfTheShipsLength = halfTheShipsLength;
            LookForCollisionDistance = lookForCollisionDistance;
            ArtificialIntelligenceInfo = artificialIntelligenceInfo;
        }

        public override Status Update()
        {
            RaycastHit HittedObject;
            bool ThereIsCollision = Physics.BoxCast(Agent.transform.position - HalfTheShipsLength * Agent.transform.forward, BoxcastDimension, ArtificialIntelligenceInfo.GetSpaceshipRotation() * Vector3.forward, 
                out HittedObject, ArtificialIntelligenceInfo.GetSpaceshipRotation(), LookForCollisionDistance, ~(1 << 8) & ~(1 << 10));
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
