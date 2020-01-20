using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

internal class AssaultArtificialIntelligence : ArtificialIntelligence
{
    protected int distanceFarFromTarget = 100;
    protected int distanceCloseToTarget = 50;
    private int dashSecureDistance = 40;
    private int dashIntensity = 25;

    private new void Start()
    {
        //Debug.unityLogger.logEnabled = false;
        base.Start();

        shipsWingspan = 10f;
        halfTheShipsLength = 7.5f;
        halfTheShipsHeight = 2.5f;

        lookForCollisionDistance = velocity + shipsWingspan * 2;

        /* TREE START */

        Parallel root = new Parallel(new List<Behavior>
        {
             new Selector(new List<Behavior>  /* SELECTOR AVOID ASTEROIR OR FACE TARGET */
             {
                 new Filter(new List<Behavior> /* FILTER AVOID ASTEROIDS */
                 {
                     new RotateTowardsPlayer(gameObject, rotationInfo),
                     new AreAsteroidsInFront(gameObject, lookForCollisionDistance, shipsWingspan, shipsWingspan, shipsWingspan, rotationInfo),
                     new Parallel(new List<Behavior> /* PARALLEL ROTATE ASTEROID */
                     {
                         new Filter(new List<Behavior> /* FILTER CHANGE ASTEROID */
                         {
                             new IsFacingNewAsteroid(gameObject, rotationInfo),
                             new ChangeFacingAsteroid(gameObject, rotationInfo),
                             new ChangeCurrentYawDirection(gameObject, rotationInfo)
                         }),
                         new While(new Sequence(new List<Behavior> /* SEQUENCE ROTATE AROUND ASTEROID */
                         {
                             new AreAsteroidsInFront(gameObject, lookForCollisionDistance, shipsWingspan, shipsWingspan, shipsWingspan, rotationInfo),
                             new Yaw(gameObject, rotationInfo)
                         }))
                     }, Parallel.Policy.RequireOne),
                     new ApplyRotation(gameObject, rotationInfo)
                 }),

                 new Sequence(new List<Behavior> /* SEQUENCE FACE TARGET AND SHOOT */
                 {
                     new ApplyRotation(gameObject, rotationInfo),

                     new Sequence(new List<Behavior> /* SEQUENCE OVERHEAT */
                     {
                         new Selector(new List<Behavior> /* SELECTOR SHOULD SHOOT */
                         {
                             new IsHealthLow(gameObject, GetComponent<DestructionController>(), healthThreshold),
                             new Invert(new IsOverheatedOrHasToWaitToShoot(gameObject, overheatData, overheatUpperThreshold, overheatLowerThreshold))
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
                                     new IsTargetInRange(gameObject, shotMaxDistance, aimingHelpRange, shotMinDistance),
                                     new IsTargetVisible(gameObject),
                                     new Shoot(gameObject, shotPrefab, shotSpeed),
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
                         new Invert(new IsObjectToTheLeft(gameObject, dashSecureDistance, halfTheShipsHeight, halfTheShipsLength)),
                         new DashLeft(gameObject, dashIntensity)
                     }),
                     new Filter(new List<Behavior> /* FILTER DASH RIGHT */
                     {
                         new Invert(new IsObjectToTheRight(gameObject, dashSecureDistance, halfTheShipsHeight, halfTheShipsLength)),
                         new DashRight(gameObject, dashIntensity)
                     }),
                 })
             }),

             new Selector(new List<Behavior> /* SELECTOR MOVE BACK OR FORWARD */
             {
                 new Sequence(new List<Behavior> /* SEQUENCE MOVE FORWARD */
                 {
                     new Selector(new List<Behavior> /* SELECTOR CONDITIONS TO MOVE FORWARD */
                     {
                         new IsTheTarjetFar(gameObject, distanceFarFromTarget),
                         new Invert(new IsTargetVisible(gameObject))
                     }),
                     new MoveForward(gameObject, velocity)
                 }),
                 new Filter(new List<Behavior> /* FILTER MOVE BACK */
                 {
                     new IsTheTarjetClose(gameObject, distanceCloseToTarget),
                     new MoveBack(gameObject, velocity)
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

        tree = new BehaviorTree.BehaviorTree(root);
    }

    private void OnDrawGizmos()
    {
        ExtDebug.DrawBoxCastBox(
          transform.position,
          new Vector3(halfTheShipsLength, halfTheShipsHeight, 0),
          transform.rotation * Quaternion.Euler(0, 90, 0),
          transform.right,
          dashSecureDistance);
    }

}
