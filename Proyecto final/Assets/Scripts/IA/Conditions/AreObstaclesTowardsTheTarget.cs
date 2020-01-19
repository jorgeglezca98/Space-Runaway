using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{

    class AreObstaclesTowardsTheTarget : LeafNode
    {

        float LookForCollisionDistance;
        float ShipsWingspan;
        float HalfTheShipsLength;
        float HalfTheShipsHeight;

        bool ThereIsCollision;

        RaycastHit HittedObject;
        RaycastHit HittedAsteroid;


        Vector3 BoxcastDimension;

        public AreObstaclesTowardsTheTarget(GameObject agent, float lookForCollisionDistance, float shipsWingspan,
                                            float halfTheShipsLength, float halfTheShipsHeight) : base(agent)
        {
            LookForCollisionDistance = lookForCollisionDistance;
            ShipsWingspan = shipsWingspan;
            HalfTheShipsLength = halfTheShipsLength;
            HalfTheShipsHeight = halfTheShipsHeight;
            BoxcastDimension = new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength);
        }

        public override Status Update()
        {
            if (ThereIsObstacleTowardsTarget())
            {
                Debug.Log("THERE IS ASTEROID towards the player");
                return Status.BH_SUCCESS;
            }
            else
            {
                Debug.Log("THERE IS NOT ASTEROID towards the player");
                return Status.BH_FAILURE;
            }
        }

        bool ThereIsObstacleTowardsTarget()
        {

            ThereIsCollision = Physics.BoxCast(Agent.transform.position, BoxcastDimension, 
                ArtificialIntelligence.Target.transform.position - Agent.transform.position, 
                out HittedObject, Agent.transform.rotation, LookForCollisionDistance, ~(1 << 8));

            if (ThereIsCollision)
            {
                if (HittedObject.collider.tag == "asteroid")
                {
                    Debug.Log("Condition says there's obstacle!");
                    HittedAsteroid = HittedObject;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }

}
