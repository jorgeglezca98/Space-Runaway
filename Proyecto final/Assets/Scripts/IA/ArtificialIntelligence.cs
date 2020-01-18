using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

namespace BehaviorTree{
	class ArtificialIntelligence : MonoBehaviour {

			public static GameObject Target;
	    public static AudioManager AudioManager;
	    protected BehaviorTree Tree;

	    protected int Velocity = 40;
			protected GameObject ShotPrefab;
	    protected int ShotMaxDistance = 100;
	    protected int ShotMinDistance = 10;
	    protected int ShotSpeed = 10000;
	    protected int AimingHelpRange = 10;
	    protected float LookForCollisionDistance = 60f;
	    protected float ShipsWingspan = 10f;
	    protected float HalfTheShipsLength = 7.5f;
	    protected float HalfTheShipsHeight = 2.5f;

			protected OverheatStats overheatData = new OverheatStats();

			// The amount of overheat the weapon produces everytime it shots.
			protected float overheatIncrement = 3f;
			// The amount of overheat the weapon cools down everytime it shots.
			protected float overheatDecrement = 0.75f;
			// The amount of time in seconds the weapon get disabled when the
			// maximum overheat is achieved.
			protected float maxOverheatPenalizationTime = 3f;
			protected bool isCoolingDown = false;

	    void FixedUpdate() {
			Tree.Tick();
		}


		public Vector3 GetSpaceshipDimension(){
					return new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength);
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
