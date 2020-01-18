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
    private float OverheatUpperThreshold;
    private float OverheatLowerThreshold;

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
        OverheatUpperThreshold = overheatData.getMaxOverheat() * 0.75f; // 75 % of overheat
        OverheatLowerThreshold = overheatData.getMaxOverheat() * 0.25f; // 25 % of overheat

        ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;

        /* TREE START */

        Parallel root = new Parallel(new List<Behavior>
        {
            new Selector(new List<Behavior>  /* SELECTOR AVOID ASTEROIR OR FACE TARGET */
            {
                new Filter(new List<Behavior> /* FILTER AVOID ASTEROIDS */
                {

                }),
                new Sequence(new List<Behavior> /* SEQUENCE FACE TARGET AND SHOOT */
                {
                    new RotateTowardsPlayer(gameObject),
                    new Parallel(new List<Behavior> /* UNTITLED PARALLEL */
                    {
                        new Sequence(new List<Behavior> /* SEQUENCE OVERHEAT */
                        {
                            new Selector(new List<Behavior> /* SELECTOR SHOULD SHOOT */
                            {
                                new IsHealthLow(gameObject, GetComponent<DestructionController>(), HealthThreshold),
                                new Invert(new IsOverheatedOrHasToWaitToShoot(gameObject, overheatData, over))
                            }),
                            new Selector(new List<Behavior> /* SELECTOR COOLING DOWN */
                            {

                            })
                        }),
                        new Filter(new List<Behavior> /* FILTER REDUCE OVERHEAT */
                        {

                        })
                    })
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

        //Tree = new BehaviorTree.BehaviorTree(root);
    }

    private void Update()
    {
        if (overheatData.getOverheat() < overheatData.getMaxOverheat() && overheatData.getOverheat() > 0)
        {
            overheatData.setOverheat(overheatData.getOverheat() - overheatDecrement);
        }
    }
}
