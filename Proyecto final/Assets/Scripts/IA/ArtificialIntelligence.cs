using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class ArtificialIntelligence : MonoBehaviour {

	public static GameObject Target;
	private BehaviorTree.BehaviorTree Tree;
    public int Velocity = 20;
	public GameObject ShotPrefab;
    public int ShotMaxDistance = 200;
    public int ShotMinDistance = 10;
    public int ShotSpeed = 2000;
    public int DistanceFarFromTarget = 100;
    public int DistanceCloseToTarget = 50;
    public int AimingHelpRange = 100;
    public float LookForCollisionDistance = 20f;
    public float ShipSpeed = 20f;
    public float ShipsWingspan = 10f;
    public float HalfTheShipsLength = 7.5f;
    public float HalfTheShipsHeight = 2.5f;

    void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		if(players.Length > 0 && Target == null)
			Target = players[0];

		Parallel root = new Parallel();

        Selector selectorMovebackOrForward = new Selector();


        Sequence sequenceRetroceder = new Sequence();
        sequenceRetroceder.AddChild(new IsTheTarjetClose(gameObject, DistanceCloseToTarget));
        sequenceRetroceder.AddChild(new MoveBack(gameObject, Velocity));
        Sequence sequenceAvanzar = new Sequence();
        sequenceAvanzar.AddChild(new IsTheTarjetFar(gameObject, DistanceFarFromTarget));
        sequenceAvanzar.AddChild(new MoveAlong(gameObject, Velocity));

        selectorMovebackOrForward.AddChild(sequenceRetroceder);
        selectorMovebackOrForward.AddChild(sequenceAvanzar);

        Sequence sequenceDashIfDamageIsReceived = new Sequence();
        sequenceDashIfDamageIsReceived.AddChild(new ShouldDash(gameObject, GetComponent<DestructionController>()));
        sequenceDashIfDamageIsReceived.AddChild(new DashMovement(gameObject));

        //root.AddChild(sequenceRetroceder);
        //root.AddChild(sequenceAvanzar);
        //root.AddChild(new Shoot(gameObject, ShotPrefab, ShotMaxDistance, ShotSpeed, AimingHelpRange, ShotMinDistance));

        Sequence sequenceShootOrAvoid = new Sequence();
        Sequence sequenceShootIfVisible = new Sequence();
        sequenceShootIfVisible.AddChild(new Shoot(gameObject, ShotPrefab, ShotMaxDistance, ShotSpeed, AimingHelpRange, ShotMinDistance));


        Selector selectorAvoidAsteroidOrFaceTarget = new Selector();

        Sequence sequenceAvoidAsteroids = new Sequence();
        sequenceAvoidAsteroids.AddChild(new AreObstaclesTowardsTheTarget(gameObject, Mathf.Infinity, ShipsWingspan,
                                                                         HalfTheShipsLength, HalfTheShipsHeight));
        sequenceAvoidAsteroids.AddChild(new RotateAroundAsteroid(gameObject, LookForCollisionDistance, ShipsWingspan,
                                                                         HalfTheShipsLength, HalfTheShipsHeight));

        selectorAvoidAsteroidOrFaceTarget.AddChild(sequenceAvoidAsteroids);
        selectorAvoidAsteroidOrFaceTarget.AddChild(new RotateTowardsPlayer(gameObject));

        sequenceShootOrAvoid.AddChild(selectorAvoidAsteroidOrFaceTarget);
        sequenceShootOrAvoid.AddChild(sequenceShootIfVisible);
        sequenceShootOrAvoid.AddChild(sequenceDashIfDamageIsReceived);
        
        root.AddChild(selectorMovebackOrForward);
        //root.AddChild(selectorAvoidAsteroidOrFaceTarget);
        root.AddChild(sequenceShootOrAvoid);





        Tree = new BehaviorTree.BehaviorTree(root);

    }

    void FixedUpdate() {
		Tree.Tick();
	}

    void OnDrawGizmos()
    {

        Vector3 direction = Target.transform.position - transform.position;

        RaycastHit HittedObject;
        bool ThereIsCollision = Physics.BoxCast(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), direction, out HittedObject, transform.rotation, Mathf.Infinity, ~(1 << 8));
        //Check if there has been a hit yet
        if (ThereIsCollision)
        {
            ExtDebug.DrawBoxCastOnHit(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), transform.rotation, direction, HittedObject.distance);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            ExtDebug.DrawBoxCastBox(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), transform.rotation, direction, HittedObject.distance);
        }

    //    bool ThereIsCollision2 = Physics.BoxCast(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), transform.forward, out HittedObject, transform.rotation, LookForCollisionDistance, ~(1 << 8));
    //    //Check if there has been a hit yet
    //    if (ThereIsCollision2)
    //    {
    //        ExtDebug.DrawBoxCastOnHit(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), transform.rotation, transform.forward, HittedObject.distance);
    //    }
    //    //If there hasn't been a hit yet, draw the ray at the maximum distance
    //    else
    //    {
    //        ExtDebug.DrawBoxCastBox(transform.position, new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength), transform.rotation, transform.forward, HittedObject.distance);
    //    }
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