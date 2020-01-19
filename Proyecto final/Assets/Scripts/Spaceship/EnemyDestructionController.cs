﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionController : DestructionController
{

    protected override void Initialize()
    {

    }

    protected override void InflictBulletDamage(string bulletType)
    {
        if (bulletType == "player_shot_prefab(Clone)")
        {
            Stats.setHealth(Stats.getHealth() - 1f);
            Debug.Log("Enemy: Receiving damage. Life : " + Stats.getHealth());
            PlayImpactSound(0f);
        }
    }


    /*It receives a parameter because the Delegate requires it*/
    protected override void PlayImpactSound(float damage)
    {
        AudioManager.PlaySoundEffect("OnEnemyImpact");
    }


    protected override void DestroySpaceship()
    {
        SplitSpaceship();
        PlayExplosionSound();
        Destroy(gameObject, destructionDelay);
        GameEventsController.eventController.enemyDestroyed();
    }
}
