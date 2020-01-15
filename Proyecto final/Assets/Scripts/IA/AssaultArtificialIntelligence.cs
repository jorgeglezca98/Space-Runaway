using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

class AssaultArtificialIntelligence : ArtificialIntelligence {

    protected int DistanceFarFromTarget = 100;
    protected int DistanceCloseToTarget = 50;
    private int DashSecureDistance = 0;
    private int DashIntensity = 10;

    void Start () {

        Target = GameObject.Find("PlayerSpaceship");
        ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

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
        sequenceDashIfDamageIsReceived.AddChild(new DashMovement(gameObject, DashSecureDistance, DashIntensity,
                                                                 HalfTheShipsHeight, HalfTheShipsLength));

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
        root.AddChild(sequenceDashIfDamageIsReceived);

        root.AddChild(sequenceShootOrAvoid);
        root.AddChild(selectorMovebackOrForward);

        Tree = new BehaviorTree.BehaviorTree(root);
    }

}
