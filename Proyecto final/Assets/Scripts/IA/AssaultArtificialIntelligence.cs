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
    private int DashIntensity = 5;
    private float HealthThreshold;
    private float OverheatUpperThreshold;
    private float OverheatLowerThreshold;
    private ArtificialIntelligenceInfo ArtificialIntelligenceInfo = new ArtificialIntelligenceInfo();

    void Start()
    {
        if (AudioManager == null)
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (Target == null)
            Target = GameObject.Find("PlayerSpaceship");

        //Debug.unityLogger.logEnabled = false;

        ShipsWingspan = 10f;
        HalfTheShipsLength = 7.5f;
        HalfTheShipsHeight = 2.5f;

        HealthThreshold = GetComponent<DestructionController>().Stats.getMaxHealth() * 0.3f; // 30 % of health
        //Debug.Log("Healt Threshold: " + HealthThreshold + ", " + GetComponent<DestructionController>().Stats.getMaxHealth());

        OverheatUpperThreshold = overheatData.getMaxOverheat() * 0.75f; // 75 % of overheat
        OverheatLowerThreshold = overheatData.getMaxOverheat() * 0.25f; // 25 % of overheat

        ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;

        Rigidbody rg = GetComponent<Rigidbody>();
        rg.drag = 0.5f;
        rg.angularDrag = 0.5f;
        rg.centerOfMass = Vector3.zero;


        /* TREE START */

        Parallel root = new Parallel(new List<Behavior>{
             new Selector(new List<Behavior>  /* SELECTOR AVOID ASTEROIR OR FACE TARGET */
             {
                 new Sequence(new List<Behavior> /* FILTER AVOID ASTEROIDS */
                 {
                    new RotateTowardsPlayer(gameObject, ArtificialIntelligenceInfo),
                    new AreAsteroidsInFront(gameObject, LookForCollisionDistance, ShipsWingspan, HalfTheShipsLength, HalfTheShipsHeight, ArtificialIntelligenceInfo),
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
                            new AreAsteroidsInFront(gameObject, LookForCollisionDistance, ShipsWingspan, HalfTheShipsLength, HalfTheShipsHeight, ArtificialIntelligenceInfo),
                            new Yaw(gameObject, ArtificialIntelligenceInfo)
                        }))
                    }),
                    new ApplyRotation(gameObject, ArtificialIntelligenceInfo)
                 }),
                 new Invert(new ApplyRotation(gameObject, ArtificialIntelligenceInfo))
             }),

             new Selector(new List<Behavior> /* SELECTOR MOVE BACK OR FORWARD */
             {
                new Sequence(new List<Behavior> /* SEQUENCE MOVE FORWARD */
                {
                    new Selector(new List<Behavior> /* SELECTOR CONDITIONS TO MOVE FORWARD */
                    {
                        new IsTheTarjetFar(gameObject, DistanceFarFromTarget),
                        new Invert(new IsTargetVisible(gameObject, ShotMaxDistance))
                    }),
                    new MoveAlong(gameObject, Velocity)
                }),
                new Filter(new List<Behavior> /* FILTER MOVE BACK */
                {
                    new IsTheTarjetClose(gameObject, DistanceCloseToTarget),
                    new MoveBack(gameObject, Velocity)
                })
             })
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
          transform.position - HalfTheShipsLength * transform.forward,
          new Vector3(ShipsWingspan * 2, HalfTheShipsHeight * 2, 0),
          ArtificialIntelligenceInfo.GetSpaceshipRotation(),
          ArtificialIntelligenceInfo.GetSpaceshipRotation() * Vector3.forward,
          LookForCollisionDistance);
    }

}
