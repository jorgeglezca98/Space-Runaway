using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

internal class KamikazeArtificialIntelligence : ArtificialIntelligence
{
    private new void Start()
    {
        //Debug.unityLogger.logEnabled = false;    
        base.Start();

        shipsWingspan = 15f;
        halfTheShipsLength = 14f;
        halfTheShipsHeight = 5f;

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

             new MoveForward(gameObject, velocity),

             new Filter(new List<Behavior> /* FILTER REDUCE OVERHEAT */
             {
                new Invert(new IsCoolingDown(gameObject, overheatData)),
                new IsOverheatGreaterThanZero(gameObject, overheatData),
                new ReduceOverheat(gameObject, overheatData)
             })
         });

        tree = new BehaviorTree.BehaviorTree(root);
    }

    void OnDrawGizmos()
    {
        ExtDebug.DrawBoxCastBox(
          transform.position - halfTheShipsLength * transform.forward,
          new Vector3(shipsWingspan * 1.5f, halfTheShipsHeight * 1.5f, 0),
          rotationInfo.SpaceshipRotation,
          rotationInfo.SpaceshipRotation * Vector3.forward,
          lookForCollisionDistance);
    }
}
