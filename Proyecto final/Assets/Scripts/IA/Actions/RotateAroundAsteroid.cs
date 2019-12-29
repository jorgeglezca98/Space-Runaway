using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAsteroid : MonoBehaviour
{

    enum Direction
    {
        Left,
        Right
    }

    public GameObject Player;
    public Vector3 PreviousAsteroidPosition;
    public Vector3 CurrentAsteroidPosition;
    private Direction CurrentDirection;
    float LookForCollisionDistance = 20f;

    bool ThereIsCollision;

    RaycastHit HittedObject;
    RaycastHit HittedAsteroid;

    float ShipSpeed = 20f;
    float ShipsWingspan = 10f;
    float HalfTheShipsLength = 7.5f;
    float HalfTheShipsHeight = 2.5f;

    Vector3 BoxcastDimension;

    // Use this for initialization
    void Start()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        BoxcastDimension = new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength);
        PreviousAsteroidPosition = new Vector3(0f, 0f, 0f);
    }

    private void FixedUpdate()
    {
        Dodge();
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(0, 0, Time.deltaTime * ShipSpeed);
    }

    void Dodge()
    {
        if (ThereIsObstacle())
        {
            if (FacingNewAsteroid())
            {
                CurrentDirection = RandomDirection();
            }

            while (ThereIsObstacle())
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
        if(direction == Direction.Right)
        {
            transform.rotation *= Quaternion.Euler(0, 1f, 0);
        }
        else
        {
            transform.rotation *= Quaternion.Euler(0, -1f, 0);
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
        Vector3 agentPosition = transform.TransformPoint(transform.position);
        Vector3 agentDirection = transform.TransformPoint(transform.position) + transform.TransformDirection(transform.forward);
        Vector3 targetPosition = transform.TransformPoint(target.transform.position);

        Vector3 agentToTarget = GenerateVectorFromPoints(agentPosition, targetPosition);
        Vector3 agentToForward = GenerateVectorFromPoints(agentPosition, agentDirection);

        Vector3 crossProduct = Vector3.Cross(agentToTarget, agentToForward).normalized;

        /* If the target is right in front of the A.I it doesn't arbitrarily it'll go left, it doesn't really matters.*/
        if ( crossProduct.y <= 0 )
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Right;
        }
    }

    float YawLeftTo(GameObject target)
    {
        Vector3 agentPosition = transform.TransformPoint(transform.position);
        Vector3 agentDirection = transform.TransformPoint(transform.position) + transform.TransformDirection(transform.forward);
        Vector3 targetPosition = transform.TransformPoint(target.transform.position);

        Vector3 agentToTarget = GenerateVectorFromPoints(agentPosition, targetPosition);
        Vector3 agentToForward = GenerateVectorFromPoints(agentPosition, agentDirection);

        Vector3 crossProduct = Vector3.Cross(agentToTarget, agentToForward).normalized;

        float angleBetweenVectors = AngleBetweenVectorsXZ(agentToTarget, agentToForward);

        if (angleBetweenVectors != 0f && crossProduct.y < 0)
        {
            return 360 - angleBetweenVectors;
        }
        else
        {
            return angleBetweenVectors;
        }
    }

    float AngleBetweenVectorsXZ(Vector3 a, Vector3 b)
    {
        float numerator = (a.x * b.x) + (a.z * b.z);
        float denominator = DistanceXZ(a) * DistanceXZ(b);
        float cos = Clamp(numerator / denominator);
        return Mathf.Acos(cos) * (180 / Mathf.PI);
    }

    float PitchLeftTo(GameObject target)
    {
        Vector3 agentPosition = transform.position;
        Vector3 agentDirection = transform.position + transform.forward;
        Vector3 targetPosition = target.transform.position;

        Vector3 agentToTarget = GenerateVectorFromPoints(agentPosition, targetPosition);
        Vector3 agentToForward = GenerateVectorFromPoints(agentPosition, agentDirection);

        Vector3 crossProduct = CrossProduct(agentToTarget, agentToForward);
        float angleBetweenVectors = AngleBetweenVectorsZY(agentToTarget, agentToForward);

        if (angleBetweenVectors != 0f && crossProduct.x > 0)
        {
            return 360 - angleBetweenVectors;
        }
        else
        {
            return angleBetweenVectors;
        }

    }

    float AngleBetweenVectorsZY(Vector3 a, Vector3 b)
    {
        float numerator = (a.z * b.z) + (a.y * b.y);
        float denominator = DistanceZY(a) * DistanceZY(b);
        float cos = numerator / denominator;
        return Mathf.Acos(cos) * (180 / Mathf.PI);
    }

    float Clamp(float value)
    {
        if (value > 1.0f)
            return 1.0f;
        else if (value < -1.0f)
            return -1.0f;
        else return value;
    }

    float DistanceXZ(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.z, 2));
    }

    float DistanceZY(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.z, 2) + Mathf.Pow(v.y, 2));
    }

    Vector3 GenerateVectorFromPoints(Vector3 a, Vector3 b)
    {
        Vector3 newVector;
        newVector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        return newVector;
    }

    Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        Vector3 crossProduct;
        crossProduct.x = a.y * b.z - a.z * b.y;
        crossProduct.y = a.x * b.z - a.z * b.x;
        crossProduct.z = a.x * b.y - a.y * b.x;
        return crossProduct;
    }

    float Round(float number, float precision)
    {
        if (Mathf.Abs(number) < precision)
        {
            return 0f;
        }
        else return number;
    }

    float CalculateSlope(Vector3 pointA, Vector3 pointB)
    {
        float numerator = pointB.x - pointA.x;
        float denominator = pointB.z - pointA.z;
        return numerator / denominator;
    }

    float DegreesToRadians(float degrees)
    {
        return Mathf.PI * (degrees / 180);
    }

    float RadiansToDegrees(float radians)
    {
        return radians * (180 / Mathf.PI);
    }

    bool ThereIsObstacle()
    {

        ThereIsCollision = Physics.BoxCast(transform.position, BoxcastDimension, transform.forward, out HittedObject, transform.rotation, LookForCollisionDistance);
        if (ThereIsCollision)
        {
            if (HittedObject.collider.tag == "asteroid")
            {
                HittedAsteroid = HittedObject;
                return true;
            }
            else return false;

        }
        else
        {
            return false;
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        //Check if there has been a hit yet
        if (ThereIsCollision)
        {
            ExtDebug.DrawBoxCastOnHit(transform.position, BoxcastDimension, transform.rotation, transform.forward, HittedObject.distance);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            ExtDebug.DrawBoxCastBox(transform.position, BoxcastDimension, transform.rotation, transform.forward, HittedObject.distance);
        }
    }
}

/*This class is purely used to draw the boxcast.*/
public static class ExtDebug
{
    //Draws just the box at where it is currently hitting.
    public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance)
    {
        origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
        DrawBox(origin, halfExtents, orientation);
    }

    //Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
    public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance)
    {
        direction.Normalize();
        Box bottomBox = new Box(origin, halfExtents, orientation);
        Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

        Gizmos.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft);
        Gizmos.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight);
        Gizmos.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft);
        Gizmos.DrawLine(bottomBox.backTopRight, topBox.backTopRight);
        Gizmos.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft);
        Gizmos.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight);
        Gizmos.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft);
        Gizmos.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight);

        DrawBox(bottomBox);
        DrawBox(topBox);
    }

    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation)
    {
        DrawBox(new Box(origin, halfExtents, orientation));
    }
    public static void DrawBox(Box box)
    {
        Gizmos.DrawLine(box.frontTopLeft, box.frontTopRight);
        Gizmos.DrawLine(box.frontTopRight, box.frontBottomRight);
        Gizmos.DrawLine(box.frontBottomRight, box.frontBottomLeft);
        Gizmos.DrawLine(box.frontBottomLeft, box.frontTopLeft);

        Gizmos.DrawLine(box.backTopLeft, box.backTopRight);
        Gizmos.DrawLine(box.backTopRight, box.backBottomRight);
        Gizmos.DrawLine(box.backBottomRight, box.backBottomLeft);
        Gizmos.DrawLine(box.backBottomLeft, box.backTopLeft);

        Gizmos.DrawLine(box.frontTopLeft, box.backTopLeft);
        Gizmos.DrawLine(box.frontTopRight, box.backTopRight);
        Gizmos.DrawLine(box.frontBottomRight, box.backBottomRight);
        Gizmos.DrawLine(box.frontBottomLeft, box.backBottomLeft);
    }

    public struct Box
    {
        public Vector3 localFrontTopLeft { get; private set; }
        public Vector3 localFrontTopRight { get; private set; }
        public Vector3 localFrontBottomLeft { get; private set; }
        public Vector3 localFrontBottomRight { get; private set; }
        public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
        public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
        public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
        public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

        public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
        public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
        public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
        public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
        public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
        public Vector3 backTopRight { get { return localBackTopRight + origin; } }
        public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
        public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

        public Vector3 origin { get; private set; }

        public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
        {
            Rotate(orientation);
        }
        public Box(Vector3 origin, Vector3 halfExtents)
        {
            this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
            this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

            this.origin = origin;
        }


        public void Rotate(Quaternion orientation)
        {
            localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
            localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
            localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
            localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
        }
    }

    //This should work for all cast types
    static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
    {
        return origin + (direction.normalized * hitInfoDistance);
    }

    static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 direction = point - pivot;
        return pivot + rotation * direction;
    }
}