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
            Stats.SetHealth(Stats.GetHealth() - 1f);
            Debug.Log("Enemy: Receiving damage. Life : " + Stats.GetHealth());
            PlayImpactSound(0f);
        }
    }


    /*It receives a parameter because the Delegate requires it*/
    protected override void PlayImpactSound(float damage)
    {
        audioManager.PlaySoundEffect("OnEnemyImpact");
    }


    protected override void DestroySpaceship()
    {
        SplitSpaceship();
        PlayExplosionSound();
        Destroy(gameObject, destructionDelay);
        GameEventsController.eventController.EnemyDestroyed();
    }
}
