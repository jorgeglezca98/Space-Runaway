using UnityEngine;

namespace BehaviorTree
{
    public class AreAsteroidsInFront : LeafNode
    {
        private Vector3 boxcastDimension;
        private float lookForCollisionDistance;
        private RotationInfo rotationInfo;
        private float halfTheShipsLength;

        public AreAsteroidsInFront(GameObject agent, float lookForCollisionDistance, float shipsWingspan, float halfTheShipsLength, float halfTheShipsHeight,
                                    RotationInfo rotationInfo) : base(agent)
        {
            boxcastDimension = new Vector3(shipsWingspan * 1.5f, halfTheShipsHeight * 1.5f, 0);
            this.halfTheShipsLength = halfTheShipsLength;
            this.lookForCollisionDistance = lookForCollisionDistance;
            this.rotationInfo = rotationInfo;
        }

        public override Status Update()
        {
            RaycastHit hittedObject;
            bool thereIsCollision = Physics.BoxCast(agent.transform.position - halfTheShipsLength * agent.transform.forward, boxcastDimension, 
                rotationInfo.SpaceshipRotation * Vector3.forward, out hittedObject, rotationInfo.SpaceshipRotation, 
                lookForCollisionDistance, ~(1 << 8) & ~(1 << 10));

            if (thereIsCollision)
            {
                if (hittedObject.collider.tag == "asteroid")
                {
                    //Debug.Log("There is AN ASTEROID in FRONT!");
                    rotationInfo.CurrentAsteroidPosition = hittedObject.transform.position;
                    return Status.BH_SUCCESS;
                }
                else
                {
                    //Debug.Log("There is something in front that is NOT an ASTEROID!");
                    return Status.BH_FAILURE;
                }
            }
            else
            {
                //Debug.Log("There is NOTHING in FRONT!");
                return Status.BH_FAILURE;
            }
        }
    }
}
