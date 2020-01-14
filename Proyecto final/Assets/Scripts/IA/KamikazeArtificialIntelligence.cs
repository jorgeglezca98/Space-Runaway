using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class KamikazeArtificialIntelligence : MonoBehaviour {

    private static GameObject Target;
    private BehaviorTree.BehaviorTree Tree;
    private int Velocity = 20;
    private GameObject ShotPrefab;
    private int ShotMaxDistance = 200;
    private int ShotMinDistance = 10;
    private int ShotSpeed = 2000;
    private int DistanceFarFromTarget = 100;
    private int DistanceCloseToTarget = 50;
    private int AimingHelpRange = 100;
    private float LookForCollisionDistance = 20f;
    private float ShipSpeed = 20f;
    private float ShipsWingspan = 10f;
    private float HalfTheShipsLength = 7.5f;
    private float HalfTheShipsHeight = 2.5f;

    void Start () {
        // GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        // if(players.Length > 0 && Target == null)
        //     Target = players[0];

        Target = GameObject.Find("PlayerSpaceship");
    		ShotPrefab = Resources.Load("shot_prefab") as GameObject;
        Parallel root = new Parallel();

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

        root.AddChild(new MoveAlong(gameObject, Velocity));
        root.AddChild(sequenceShootOrAvoid);

        Tree = new BehaviorTree.BehaviorTree(root);

    }

    void FixedUpdate() {
        Tree.Tick();
    }
}
