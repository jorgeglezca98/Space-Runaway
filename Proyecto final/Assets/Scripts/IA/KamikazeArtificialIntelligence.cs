using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

class KamikazeArtificialIntelligence : ArtificialIntelligence {

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
}
