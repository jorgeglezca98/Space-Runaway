using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

class KamikazeArtificialIntelligence : ArtificialIntelligence {

    void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length > 0 && Target == null)
            Target = players[0];
        
        if(AudioManager == null)
          AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if(Target == null)
          Target = GameObject.Find("PlayerSpaceship");
        
        ShipsWingspan = 18f;
        HalfTheShipsLength = 14f;
        HalfTheShipsHeight = 5f;
        
    	  ShotPrefab = Resources.Load("enemy_shot_prefab") as GameObject;
        
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

             new MoveAlong(gameObject, Velocity),

             new Filter(new List<Behavior> /* FILTER REDUCE OVERHEAT */
             {
                new Invert(new IsCoolingDown(gameObject, overheatData)),
                new IsOverheatGreaterThanZero(gameObject, overheatData),
                new ReduceOverheat(gameObject, overheatData)
             })
         });
        
        Tree = new BehaviorTree.BehaviorTree(root);
    }
}
