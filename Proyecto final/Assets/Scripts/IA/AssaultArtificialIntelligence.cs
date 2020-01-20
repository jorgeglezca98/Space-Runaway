using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class AssaultArtificialIntelligence : ArtificialIntelligence
{
    protected int distanceFarFromTarget = 100;
    protected int distanceCloseToTarget = 50;
    private int dashSecureDistance = 40;
    private int dashIntensity = 25;

    private void Start()
    {
        lookForCollisionDistance = velocity + shipsWingspan * 2;
        Debug.unityLogger.logEnabled = false;

        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        if (target == null)
        {
            target = GameObject.Find("PlayerSpaceship");
        }

        shipsWingspan = 10f;
        halfTheShipsLength = 7.5f;
        halfTheShipsHeight = 2.5f;

        healthThreshold = GetComponent<DestructionController>().Stats.GetMaxHealth() * 0.3f; // 30 % of health

        overheatUpperThreshold = overheatData.GetMaxOverheat() * 0.75f;
        overheatLowerThreshold = overheatData.GetMaxOverheat() * 0.25f;

        shotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;

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

        tree = new BehaviorTree.BehaviorTree(root);
    }

    private void OnDrawGizmos()
    {
        ExtDebug.DrawBoxCastBox(
<<<<<<< HEAD
          transform.position - HalfTheShipsLength * transform.forward,
          new Vector3(ShipsWingspan, HalfTheShipsHeight, HalfTheShipsLength),
          transform.rotation,
          transform.forward,
          LookForCollisionDistance);
    }

    // bool ThereIsCollision = Physics.BoxCast(Agent.transform.position - HalfTheShipsLength * Agent.transform.forward, BoxcastDimension, Agent.transform.forward,
    //     out HittedObject, Agent.transform.rotation, LookForCollisionDistance, ~(1 << 8) & ~(1 << 10));

=======
          transform.position,
          new Vector3(halfTheShipsLength, halfTheShipsHeight, 0),
          transform.rotation * Quaternion.Euler(0, 90, 0),
          transform.right,
          dashSecureDistance
       );
    }
>>>>>>> 0e82ac487e4a1879ab9fb0936555ccf09b3a29b9
}
