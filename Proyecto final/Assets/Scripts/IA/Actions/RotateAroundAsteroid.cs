using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundAsteroid : MonoBehaviour {

    public GameObject player;

    private Vector3 boxcast;
    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;

    Vector3 boxcastDimension;
    Vector3 directionToPlayer;

    // Use this for initialization
    void Start () {
        m_MaxDistance = 20f;
        boxcastDimension = new Vector3(14f,5f,15f);

        //Debug.Log("Vector3 up: " + transform.up);


        float angleToTargetInY = YawLeft();
        Debug.Log("Pre Angle Y: " + angleToTargetInY);

        float angleToTargetInX = PitchLeft();
        Debug.Log("Pre Angle X: " + angleToTargetInX);

        if (angleToTargetInY != 0f)
            transform.eulerAngles = new Vector3(0, angleToTargetInY, 0);

        angleToTargetInY = YawLeft();
        Debug.Log("Post Angle Y: " + angleToTargetInY);

        angleToTargetInX = PitchLeft();
        Debug.Log("Post Angle X: " + angleToTargetInX);

        


        //if (angleToTargetInY != 0f)
        //    transform.eulerAngles = new Vector3(0, angleToTargetInY, 0);

        //if (angleToTargetInX != 0f)
        //    transform.eulerAngles = new Vector3(angleToTargetInX, 0,0);
        //angleToTargetInX = AngleToTargetInX();
        //Debug.Log("Post angle: " + angleToTargetInX);


        //float angleToTargetInY = AngleToTargetInY();
        //Debug.Log("Necessary Angle to face the Target: " + angleToTargetInY);
        //if (angleToTargetInY != 0f)
        //    transform.eulerAngles = new Vector3(0, angleToTargetInY, 0);
        //angleToTargetInY = AngleToTargetInY();
        //Debug.Log("Necessary Angle to face the Target: " + angleToTargetInY);
    }

    float YawLeft()
    {
        Vector3 agentPosition = transform.position;
        Vector3 agentDirection = transform.position + transform.forward;
        Vector3 targetPosition = player.transform.position;

        Vector3 agentToTarget = GenerateVectorFromPoints(agentPosition, targetPosition);
        Vector3 agentToForward = GenerateVectorFromPoints(agentPosition, agentDirection);

        Vector3 crossProduct = CrossProduct(agentToTarget, agentToForward);
        float angleBetweenVectors = AngleBetweenVectorsXZ(agentToTarget, agentToForward);

        //Debug.Log("Angle Between Vectors: " + angleBetweenVectors);

        if(angleBetweenVectors != 0f && crossProduct.y < 0)
        {
            //Debug.Log("Angle1: " + (360 - angleBetweenVectors));
            return 360 - angleBetweenVectors;
        }
        else
        {
            //Debug.Log("Angle2: " + (angleBetweenVectors));
            return angleBetweenVectors;
        }
    }

    float PitchLeft()
    {
        Vector3 agentPosition = transform.position;
        Vector3 agentDirection = transform.position + transform.forward;
        Vector3 targetPosition = player.transform.position;

        Vector3 agentToTarget = GenerateVectorFromPoints(agentPosition, targetPosition);
        Vector3 agentToForward = GenerateVectorFromPoints(agentPosition, agentDirection);

        Vector3 crossProduct = CrossProduct(agentToTarget, agentToForward);
        float angleBetweenVectors = AngleBetweenVectorsZY(agentToTarget, agentToForward);

        if (angleBetweenVectors != 0f && crossProduct.x > 0)
        {
            //Debug.Log("Angle1: " + (360 - angleBetweenVectors));
            return 360 - angleBetweenVectors;
        }
        else
        {
            //Debug.Log("Angle2: " + (angleBetweenVectors));
            return angleBetweenVectors;
        }

    }


    float AngleBetweenVectorsZY(Vector3 a, Vector3 b)
    {
        float numerator = (a.z * b.z) + (a.y * b.y);
        float denominator = DistanceZY(a) * DistanceZY(b);

        //Debug.Log("a: " + a);
        //Debug.Log("b: " + b);
        //Debug.Log("Numerator: " + numerator);
        //Debug.Log("Denominator: " + denominator);
        float cos = numerator / denominator;
        return Mathf.Acos(cos) * (180 / Mathf.PI);
    }

    float AngleBetweenVectorsXZ(Vector3 a, Vector3 b)
    {
        float numerator = (a.x * b.x) + (a.z * b.z);
        float denominator = DistanceXZ(a) * DistanceXZ(b);

        //Debug.Log("a: " + a);
        //Debug.Log("b: " + b);
        //Debug.Log("Numerator: " + numerator);
        //Debug.Log("Denominator: " + denominator);
        float cos = numerator / denominator;
        return Mathf.Acos(cos) * (180 / Mathf.PI);
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

    float Round(float number, float precision){
        if (Mathf.Abs(number) < precision){
            return 0f;
        }else return number;
    }

    float calculateSlope(Vector3 pointA, Vector3 pointB)
    {
        float numerator = pointB.x - pointA.x;
        float denominator = pointB.z - pointA.z;
        return numerator / denominator;
    }

    float degreesToRadians(float degrees)
    {
        return Mathf.PI * (degrees / 180);
    }

    float RadiansToDegrees(float radians)
    {
        return radians * (180 / Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        directionToPlayer = (player.transform.position - transform.position);
        //rotationAngleAroundY();
       // transform.Rotate(0.0f, (float)rotationAngleAroundY(), 0.0f, Space.Self);


        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, Mathf.Infinity, layerMask))
        {

           // Debug.Log("Player position: " + player.transform.position);
            Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.yellow);
            // if (hit.transform.tag == "asteroid")
            //   Debug.Log("Bingo!");
            // Debug.Log("Hit a : " + hit.transform.tag);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //    Debug.Log("Hit a : " + hit.transform.tag);
        }
    }

        //       //Test to see if there is a hit using a BoxCast
        //       //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //       //Also fetch the hit data
        //       //m_HitDetect = Physics.BoxCast(transform.position, boxcastDimension, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
        //       //if (m_HitDetect)
        //       //{
        //       //    //Output the name of the Collider your Box hit
        //       //    Debug.Log("Hit : " + m_Hit.collider.name);
        //       //}
        //   }

        //   double rotationAngleAroundY()
        //   {
        //       float xOffset = transform.position.x - player.transform.position.x;
        //       float zOffset = transform.position.z - player.transform.position.z;
        //       Debug.Log("X offset: " + xOffset);
        //       Debug.Log("Z offset: " + zOffset);
        //       double angleInRadians = Mathf.Atan(xOffset / zOffset);
        //       double angleInDegrees = angleInRadians * (180/Mathf.PI);
        //       Debug.Log(angleInDegrees);
        //       return angleInDegrees;

        //   }

        //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
        //void OnDrawGizmos()
        //{
        //    //Check if there has been a hit yet
        //    if (m_HitDetect)
        //    {
        //        Gizmos.color = Color.red;
        //        //Draw a Ray forward from GameObject toward the hit
        //        Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
        //        //Draw a cube that extends to where the hit exists
        //        Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, boxcastDimension);
        //    }
        //    //If there hasn't been a hit yet, draw the ray at the maximum distance
        //    else
        //    {
        //        Gizmos.color = Color.green;
        //        //Draw a Ray forward from GameObject toward the maximum distance
        //        Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
        //        //Draw a cube at the maximum distance
        //        Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, boxcastDimension);
        //    }
        //}



    }

