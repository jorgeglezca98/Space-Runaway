using UnityEngine;

namespace BehaviorTree
{
    public class AreObstaclesTowardsTheTarget : LeafNode
    {
        private float lookForCollisionDistance;
        private float shipsWingspan;
        private float halfTheShipsLength;
        private float halfTheShipsHeight;

        private Vector3 boxcastDimension;

        public AreObstaclesTowardsTheTarget(GameObject agent, float lookForCollisionDistance, float shipsWingspan,
                                            float halfTheShipsLength, float halfTheShipsHeight) : base(agent)
        {
            this.lookForCollisionDistance = lookForCollisionDistance;
            this.shipsWingspan = shipsWingspan;
            this.halfTheShipsLength = halfTheShipsLength;
            this.halfTheShipsHeight = halfTheShipsHeight;
            boxcastDimension = new Vector3(this.shipsWingspan, this.halfTheShipsHeight, this.halfTheShipsLength);
        }

        public override Status Update()
        {
            if (ThereIsObstacleTowardsTarget())
            {
                //Debug.Log("THERE IS ASTEROID towards the player");
                return Status.BH_SUCCESS;
            }
            else
            {
                //Debug.Log("THERE IS NOT ASTEROID towards the player");
                return Status.BH_FAILURE;
            }
        }

        private bool ThereIsObstacleTowardsTarget()
        {
            RaycastHit hittedObject;

            bool thereIsCollision = Physics.BoxCast(agent.transform.position, boxcastDimension,
                    ArtificialIntelligence.target.transform.position - agent.transform.position,
                    out hittedObject, agent.transform.rotation, lookForCollisionDistance, ~(1 << 8));

            if (thereIsCollision)
            {
                if (hittedObject.collider.tag == "asteroid")
                {
                    //Debug.Log("Condition says there's obstacle!");
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
