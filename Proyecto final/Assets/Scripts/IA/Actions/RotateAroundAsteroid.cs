using UnityEngine;

namespace BehaviorTree
{
    class RotateAroundAsteroid : LeafNode
    {
        enum Direction
        {
            Left,
            Right
        }

        public Vector3 PreviousAsteroidPosition;
        public Vector3 CurrentAsteroidPosition;
        private Direction CurrentDirection;

        float LookForCollisionDistance;
        float ShipsWingspan;
        float HalfTheShipsLength;
        float HalfTheShipsHeight;

        bool ThereIsCollision;

        RaycastHit HittedObject;
        RaycastHit HittedAsteroid;


        Vector3 BoxcastDimension;

        public RotateAroundAsteroid(GameObject agent, float lookForCollisionDistance,
                                    float shipsWingspan, float halfTheShipsLength,
                                    float halfTheShipsHeight) : base(agent)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            ShipsWingspan = shipsWingspan;
            HalfTheShipsLength = halfTheShipsLength;
            HalfTheShipsHeight = halfTheShipsHeight;
            LookForCollisionDistance = lookForCollisionDistance;
            BoxcastDimension = new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength);
            PreviousAsteroidPosition = new Vector3(0f, 0f, 0f);
        }

        public override Status Update()
        {
            Dodge();
            return Status.BH_SUCCESS;
        }

        void Dodge()
        {
            if (ThereIsObstacleInFront())
            {
              Debug.Log("Action says there's obstacle!");
                if (FacingNewAsteroid())
                {
                    CurrentDirection = DirectionTo(ArtificialIntelligence.Target);
                }
                while (ThereIsObstacleInFront())
                    Yaw(CurrentDirection);
            }
        }

        Direction RandomDirection()
        {
            int randomNumber = Random.Range(-1, 1);
            if (randomNumber >= 0)
                return Direction.Right;
            else return Direction.Left;
        }

        bool FacingNewAsteroid()
        {
            CurrentAsteroidPosition = HittedAsteroid.transform.position;
            if (CurrentAsteroidPosition != PreviousAsteroidPosition)
            {
                PreviousAsteroidPosition = CurrentAsteroidPosition;
                return true;
            }
            else return false;
        }

        void Yaw(Direction direction)
        {
            if (direction == Direction.Right)
            {
                Agent.transform.rotation *= Quaternion.Euler(0, 1f, 0);
            }
            else
            {
                Agent.transform.rotation *= Quaternion.Euler(0, -1f, 0);
            }
        }

        Direction InverseDirection(Direction direction)
        {
            if (direction == Direction.Left)
                return Direction.Right;
            else return Direction.Left;
        }

        Direction DirectionTo(GameObject target)
        {
            float angle1 = Quaternion.Angle(Agent.transform.rotation * Quaternion.Euler(0f, 45f, 0f), target.transform.rotation) % 360;
            float angle2 = Quaternion.Angle(Agent.transform.rotation * Quaternion.Euler(0f, -45f, 0f), target.transform.rotation) % 360;
            if (angle1 < angle2)
            {
                return Direction.Right;
            }
            else return Direction.Left;
        }


        bool ThereIsObstacleInFront()
        {
            ThereIsCollision = Physics.BoxCast(Agent.transform.position, BoxcastDimension, Agent.transform.forward, out HittedObject, Agent.transform.rotation, LookForCollisionDistance);
            if (ThereIsCollision)
            {
                if (HittedObject.collider.tag == "asteroid")
                {
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

        //bool ThereIsObstacleBetweenUser()
        //{

        //    ThereIsCollision = Physics.BoxCast(transform.position, BoxcastDimension, Player.transform.position , out HittedObject, transform.rotation, LookForCollisionDistance);
        //    if (ThereIsCollision)
        //    {
        //        if (HittedObject.collider.tag == "asteroid")
        //        {
        //            HittedAsteroid = HittedObject;
        //            return true;
        //        }
        //        else return false;

        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
        //    void OnDrawGizmos()
        //    {
        //        //Check if there has been a hit yet
        //        if (ThereIsCollision)
        //        {
        //            ExtDebug.DrawBoxCastOnHit(transform.position, BoxcastDimension, transform.rotation, transform.forward, HittedObject.distance);
        //        }
        //        //If there hasn't been a hit yet, draw the ray at the maximum distance
        //        else
        //        {
        //            ExtDebug.DrawBoxCastBox(transform.position, BoxcastDimension, transform.rotation, transform.forward, HittedObject.distance);
        //        }
        //    }
        //}
    }
}





/*This class is purely used to draw the boxcast.*/
//public static class ExtDebug
//{
//    //Draws just the box at where it is currently hitting.
//    public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance)
//    {
//        origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
//        DrawBox(origin, halfExtents, orientation);
//    }

//    //Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
//    public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance)
//    {
//        direction.Normalize();
//        Box bottomBox = new Box(origin, halfExtents, orientation);
//        Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

//        Gizmos.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft);
//        Gizmos.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight);
//        Gizmos.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft);
//        Gizmos.DrawLine(bottomBox.backTopRight, topBox.backTopRight);
//        Gizmos.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft);
//        Gizmos.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight);
//        Gizmos.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft);
//        Gizmos.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight);

//        DrawBox(bottomBox);
//        DrawBox(topBox);
//    }

//    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation)
//    {
//        DrawBox(new Box(origin, halfExtents, orientation));
//    }
//    public static void DrawBox(Box box)
//    {
//        Gizmos.DrawLine(box.frontTopLeft, box.frontTopRight);
//        Gizmos.DrawLine(box.frontTopRight, box.frontBottomRight);
//        Gizmos.DrawLine(box.frontBottomRight, box.frontBottomLeft);
//        Gizmos.DrawLine(box.frontBottomLeft, box.frontTopLeft);

//        Gizmos.DrawLine(box.backTopLeft, box.backTopRight);
//        Gizmos.DrawLine(box.backTopRight, box.backBottomRight);
//        Gizmos.DrawLine(box.backBottomRight, box.backBottomLeft);
//        Gizmos.DrawLine(box.backBottomLeft, box.backTopLeft);

//        Gizmos.DrawLine(box.frontTopLeft, box.backTopLeft);
//        Gizmos.DrawLine(box.frontTopRight, box.backTopRight);
//        Gizmos.DrawLine(box.frontBottomRight, box.backBottomRight);
//        Gizmos.DrawLine(box.frontBottomLeft, box.backBottomLeft);
//    }

//    public struct Box
//    {
//        public Vector3 localFrontTopLeft { get; private set; }
//        public Vector3 localFrontTopRight { get; private set; }
//        public Vector3 localFrontBottomLeft { get; private set; }
//        public Vector3 localFrontBottomRight { get; private set; }
//        public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
//        public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
//        public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
//        public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

//        public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
//        public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
//        public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
//        public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
//        public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
//        public Vector3 backTopRight { get { return localBackTopRight + origin; } }
//        public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
//        public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

//        public Vector3 origin { get; private set; }

//        public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
//        {
//            Rotate(orientation);
//        }
//        public Box(Vector3 origin, Vector3 halfExtents)
//        {
//            this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
//            this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
//            this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
//            this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

//            this.origin = origin;
//        }


//        public void Rotate(Quaternion orientation)
//        {
//            localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
//            localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
//            localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
//            localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
//        }
//    }

//    //This should work for all cast types
//    static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
//    {
//        return origin + (direction.normalized * hitInfoDistance);
//    }

//    static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
//    {
//        Vector3 direction = point - pivot;
//        return pivot + rotation * direction;
//    }
//}
