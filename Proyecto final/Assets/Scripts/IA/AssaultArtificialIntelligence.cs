using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

class AssaultArtificialIntelligence : ArtificialIntelligence
{
    protected int DistanceFarFromTarget = 100;
    protected int DistanceCloseToTarget = 50;
    private int DashSecureDistance = 0;
    private int DashIntensity = 10;
    private float HealthThreshold;
    private float OverheatThreshold;

    void Start()
    {
        if (AudioManager == null)
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (Target == null)
            Target = GameObject.Find("PlayerSpaceship");


        ShipsWingspan = 10f;
        HalfTheShipsLength = 7.5f;
        HalfTheShipsHeight = 2.5f;

        HealthThreshold = GetComponent<DestructionController>().Stats.getMaxHealth() * 0.3f; // 30 % of health
        OverheatThreshold = overheatData.getMaxOverheat() * 0.75f; // 75 % of overheat

        ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;

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

        sequenceShootIfVisible.AddChild(new IsNotOverheatedOrLifeIsLow(gameObject, GetComponent<DestructionController>(), overheatData,
            OverheatThreshold, HealthThreshold));
        sequenceShootIfVisible.AddChild(new Shoot(gameObject, ShotPrefab, ShotMaxDistance, ShotSpeed, AimingHelpRange, ShotMinDistance,
            overheatIncrement, overheatDecrement, maxOverheatPenalizationTime, overheatData));

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

    private void Update()
    {
        if (overheatData.getOverheat() < overheatData.getMaxOverheat() && overheatData.getOverheat() > 0)
        {
            overheatData.setOverheat(overheatData.getOverheat() - overheatDecrement);
        }
    }
}
