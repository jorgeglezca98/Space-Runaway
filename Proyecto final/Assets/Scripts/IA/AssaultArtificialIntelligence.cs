using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

class AssaultArtificialIntelligence : ArtificialIntelligence
{
    protected int DistanceFarFromTarget = 100;
    protected int DistanceCloseToTarget = 50;
    private int DashSecureDistance = 40;
    private int DashIntensity = 25;

    void Start()
    {
        LookForCollisionDistance = Velocity + ShipsWingspan * 2;
        Debug.unityLogger.logEnabled = false;

        if (AudioManager == null)
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (Target == null)
            Target = GameObject.Find("PlayerSpaceship");

        ShipsWingspan = 10f;
        HalfTheShipsLength = 7.5f;
        HalfTheShipsHeight = 2.5f;

        HealthThreshold = GetComponent<DestructionController>().Stats.getMaxHealth() * 0.3f; // 30 % of health

        OverheatUpperThreshold = overheatData.getMaxOverheat() * 0.75f;
        OverheatLowerThreshold = overheatData.getMaxOverheat() * 0.25f;

        ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;

        Rigidbody rg = GetComponent<Rigidbody>();
        rg.drag = 2f;
        rg.angularDrag = 0.5f;
        rg.centerOfMass = Vector3.zero;
        /* TREE START */

        Parallel root = new Parallel(new List<Behavior>
        {
             new Selector(new List<Behavior>  /* SELECTOR AVOID ASTEROIR OR FACE TARGET */
             {
                 new Filter(new List<Behavior> /* FILTER AVOID ASTEROIDS */
                 {
                     new RotateTowardsPlayer(gameObject, ArtificialIntelligenceInfo),
                     new AreAsteroidsInFront(gameObject, LookForCollisionDistance, ShipsWingspan, ShipsWingspan, ShipsWingspan, ArtificialIntelligenceInfo),
                     new Parallel(new List<Behavior> /* PARALLEL ROTATE ASTEROID */
                     {
                         new Filter(new List<Behavior> /* FILTER CHANGE ASTEROID */
                         {
                             new IsFacingNewAsteroid(gameObject, ArtificialIntelligenceInfo),
                             new ChangeFacingAsteroid(gameObject, ArtificialIntelligenceInfo),
                             new ChangeCurrentYawDirection(gameObject, ArtificialIntelligenceInfo)
                         }),
                         new While(new Sequence(new List<Behavior> /* SEQUENCE ROTATE AROUND ASTEROID */
                         {
                             new AreAsteroidsInFront(gameObject, LookForCollisionDistance, ShipsWingspan, ShipsWingspan, ShipsWingspan, ArtificialIntelligenceInfo),
                             new Yaw(gameObject, ArtificialIntelligenceInfo)
                         }))
                     }, Parallel.Policy.RequireOne),
                     new ApplyRotation(gameObject, ArtificialIntelligenceInfo)
                 }),

                 new Sequence(new List<Behavior> /* SEQUENCE FACE TARGET AND SHOOT */
                 {
                     new ApplyRotation(gameObject, ArtificialIntelligenceInfo),

                     new Sequence(new List<Behavior> /* SEQUENCE OVERHEAT */
                     {
                         new Selector(new List<Behavior> /* SELECTOR SHOULD SHOOT */
                         {
                             new IsHealthLow(gameObject, GetComponent<DestructionController>(), HealthThreshold),
                             new Invert(new IsOverheatedOrHasToWaitToShoot(gameObject, overheatData, OverheatUpperThreshold, OverheatLowerThreshold))
                         }),
                         new Selector(new List<Behavior> /* SELECTOR COOLING DOWN */
                         {
                             new Filter(new List<Behavior> /* FILTER COOLING DOWN */
                             {
                                 new IsCoolingDown(gameObject, overheatData),
                                 new IsCoolingDownDone(gameObject, overheatData),
                                 new EndCooldown(gameObject, overheatData)
                             }),
                             new Selector(new List<Behavior> /* SELECTOR NOT COOLING DOWN */
                             {
                                 new Sequence(new List<Behavior> /* SEQUENCE IF OVERHEAT COOL DOWN */
                                 {
                                     new IsOverheated(gameObject, overheatData),
                                     new StartCooldown(gameObject, overheatData)
                                 }),
                                 new Filter(new List<Behavior> /* FILTER SHOOT IF VISIBLE */
                                 {
                                     new IsTargetInRange(gameObject, ShotMaxDistance, AimingHelpRange, ShotMinDistance),
                                     new IsTargetVisible(gameObject),
                                     new Shoot(gameObject, ShotPrefab, ShotSpeed),
                                     new IncreaseOverheat(gameObject, overheatData)
                                 })
                             })
                         })
                     }),
                 })
             }),

             new Filter(new List<Behavior> /* FILTER DASH */
             {
                 new ShouldDash(gameObject, GetComponent<DestructionController>()),
                 new Selector(new List<Behavior> /* SELECTOR DASH LEFT OR DASH RIGHT */
                 {
                     new Filter(new List<Behavior> /* FILTER DASH LEFT */
                     {
                         new Invert(new IsObjectToTheLeft(gameObject, DashSecureDistance, HalfTheShipsHeight, HalfTheShipsLength)),
                         new DashLeft(gameObject, DashIntensity)
                     }),
                     new Filter(new List<Behavior> /* FILTER DASH RIGHT */
                     {
                         new Invert(new IsObjectToTheRight(gameObject, DashSecureDistance, HalfTheShipsHeight, HalfTheShipsLength)),
                         new DashRight(gameObject, DashIntensity)
                     }),
                 })
             }),

             new Selector(new List<Behavior> /* SELECTOR MOVE BACK OR FORWARD */
             {
                 new Sequence(new List<Behavior> /* SEQUENCE MOVE FORWARD */
                 {
                     new Selector(new List<Behavior> /* SELECTOR CONDITIONS TO MOVE FORWARD */
                     {
                         new IsTheTarjetFar(gameObject, DistanceFarFromTarget),
                         new Invert(new IsTargetVisible(gameObject))
                     }),
                     new MoveAlong(gameObject, Velocity)
                 }),
                 new Filter(new List<Behavior> /* FILTER MOVE BACK */
                 {
                     new IsTheTarjetClose(gameObject, DistanceCloseToTarget),
                     new MoveBack(gameObject, Velocity)
                 })
             }),

             new Filter(new List<Behavior> /* FILTER REDUCE OVERHEAT */
             {
                new Invert(new IsCoolingDown(gameObject, overheatData)),
                new IsOverheatGreaterThanZero(gameObject, overheatData),
                new ReduceOverheat(gameObject, overheatData)
             }),
         });


        //Selector selectorMovebackOrForward = new Selector();

        //Sequence sequenceRetroceder = new Sequence();
        //sequenceRetroceder.AddChild(new IsTheTarjetClose(gameObject, DistanceCloseToTarget));
        //sequenceRetroceder.AddChild(new MoveBack(gameObject, Velocity));
        //Sequence sequenceAvanzar = new Sequence();
        //sequenceAvanzar.AddChild(new IsTheTarjetFar(gameObject, DistanceFarFromTarget));
        //sequenceAvanzar.AddChild(new MoveAlong(gameObject, Velocity));

        //selectorMovebackOrForward.AddChild(sequenceRetroceder);
        //selectorMovebackOrForward.AddChild(sequenceAvanzar);

        //Sequence sequenceDashIfDamageIsReceived = new Sequence();
        //sequenceDashIfDamageIsReceived.AddChild(new ShouldDash(gameObject, GetComponent<DestructionController>()));
        //sequenceDashIfDamageIsReceived.AddChild(new DashMovement(gameObject, DashSecureDistance, DashIntensity,
        //                                                         HalfTheShipsHeight, HalfTheShipsLength));

        //Sequence sequenceShootOrAvoid = new Sequence();
        //Sequence sequenceShootIfVisible = new Sequence();

        //sequenceShootIfVisible.AddChild(new IsNotOverheatedOrLifeIsLow(gameObject, GetComponent<DestructionController>(), overheatData,
        //    OverheatThreshold, HealthThreshold));
        //sequenceShootIfVisible.AddChild(new Shoot(gameObject, ShotPrefab, ShotMaxDistance, ShotSpeed, AimingHelpRange, ShotMinDistance,
        //    overheatIncrement, overheatDecrement, maxOverheatPenalizationTime, overheatData));

        //Selector selectorAvoidAsteroidOrFaceTarget = new Selector();

        //Sequence sequenceAvoidAsteroids = new Sequence();
        //sequenceAvoidAsteroids.AddChild(new AreObstaclesTowardsTheTarget(gameObject, Mathf.Infinity, ShipsWingspan,
        //                                                                 HalfTheShipsLength, HalfTheShipsHeight));
        //sequenceAvoidAsteroids.AddChild(new RotateAroundAsteroid(gameObject, LookForCollisionDistance, ShipsWingspan,
        //                                                                 HalfTheShipsLength, HalfTheShipsHeight));

        //selectorAvoidAsteroidOrFaceTarget.AddChild(sequenceAvoidAsteroids);
        //selectorAvoidAsteroidOrFaceTarget.AddChild(new RotateTowardsPlayer(gameObject));

        //sequenceShootOrAvoid.AddChild(selectorAvoidAsteroidOrFaceTarget);
        //sequenceShootOrAvoid.AddChild(sequenceShootIfVisible);
        //root.AddChild(sequenceDashIfDamageIsReceived);

        //root.AddChild(sequenceShootOrAvoid);
        //root.AddChild(selectorMovebackOrForward);

        Tree = new BehaviorTree.BehaviorTree(root);
    }

    void OnDrawGizmos()
    {
        ExtDebug.DrawBoxCastBox(
          transform.position,
          new Vector3(HalfTheShipsLength, HalfTheShipsHeight, 0),
          transform.rotation * Quaternion.Euler(0, 90, 0),
          transform.right,
          DashSecureDistance);
    }

}
