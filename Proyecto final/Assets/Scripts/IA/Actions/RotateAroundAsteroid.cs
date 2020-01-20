using UnityEngine;

namespace BehaviorTree
{
    public class RotateAroundAsteroid : LeafNode
    {
        private enum Direction
        {
            Left,
            Right
        }

        private Direction currentDirection;
        private float lookForCollisionDistance;
        private float shipsWingspan;
        private float halfTheShipsLength;
        private float halfTheShipsHeight;
        private bool thereIsCollision;
        private RaycastHit hittedObject;
        private RaycastHit hittedAsteroid;
        private Vector3 boxcastDimension;

        public Vector3 PreviousAsteroidPosition { get; set; }
        public Vector3 CurrentAsteroidPosition { get; set; }

        public RotateAroundAsteroid(GameObject agent, float lookForCollisionDistance,
                                    float shipsWingspan, float halfTheShipsLength,
                                    float halfTheShipsHeight) : base(agent)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            this.shipsWingspan = shipsWingspan;
            this.halfTheShipsLength = halfTheShipsLength;
            this.halfTheShipsHeight = halfTheShipsHeight;
            this.lookForCollisionDistance = lookForCollisionDistance;
            boxcastDimension = new Vector3(this.shipsWingspan, this.halfTheShipsHeight, this.halfTheShipsLength);
            PreviousAsteroidPosition = new Vector3(0f, 0f, 0f);
        }

        public override Status Update()
        {
            Dodge();
            return Status.BH_SUCCESS;
        }

        private void Dodge()
        {
            if (ThereIsObstacleInFront())
            {
                Debug.Log("Action says there's obstacle!");
                if (FacingNewAsteroid())
                {
                    currentDirection = DirectionTo(ArtificialIntelligence.target);
                }
                while (ThereIsObstacleInFront())
                {
                    Yaw(currentDirection);
                }
            }
        }

        private Direction RandomDirection()
        {
            int randomNumber = Random.Range(-1, 1);
            if (randomNumber >= 0)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }

        private bool FacingNewAsteroid()
        {
            CurrentAsteroidPosition = hittedAsteroid.transform.position;
            if (CurrentAsteroidPosition != PreviousAsteroidPosition)
            {
                PreviousAsteroidPosition = CurrentAsteroidPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Yaw(Direction direction)
        {
            if (direction == Direction.Right)
            {
                agent.transform.rotation *= Quaternion.Euler(0, 1f, 0);
            }
            else
            {
                agent.transform.rotation *= Quaternion.Euler(0, -1f, 0);
            }
        }

        private Direction InverseDirection(Direction direction)
        {
            if (direction == Direction.Left)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }

        private Direction DirectionTo(GameObject target)
        {
            float angle1 = Quaternion.Angle(agent.transform.rotation * Quaternion.Euler(0f, 45f, 0f), target.transform.rotation) % 360;
            float angle2 = Quaternion.Angle(agent.transform.rotation * Quaternion.Euler(0f, -45f, 0f), target.transform.rotation) % 360;
            if (angle1 < angle2)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }

        private bool ThereIsObstacleInFront()
        {
            thereIsCollision = Physics.BoxCast(agent.transform.position, boxcastDimension, agent.transform.forward, out hittedObject, agent.transform.rotation, lookForCollisionDistance);
            if (thereIsCollision)
            {
                if (hittedObject.collider.tag == "asteroid")
                {
                    hittedAsteroid = hittedObject;
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
