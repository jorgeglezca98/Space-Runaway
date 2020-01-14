using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class KamikazeArtificialIntelligence : MonoBehaviour {

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