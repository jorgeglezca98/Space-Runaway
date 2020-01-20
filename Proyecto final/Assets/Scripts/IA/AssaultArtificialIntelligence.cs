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
             new Selector(new List<Behavior>  /* SELECTOR AVOID ASTEROIR OR SHOOT */
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
                         new While(new Filter(new List<Behavior> /* FILTER ROTATE AROUND ASTEROID */
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

                     new Sequence(new List<Behavior> /* SEQUENCE SHOOT */
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
                                 new Filter(new List<Behavior> /* FILTER IF OVERHEAT COOL DOWN */
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
                         new Invert(new IsTargetVisibleInFront(gameObject))
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

        tree = new BehaviorTree.BehaviorTree(root);
    }
}
