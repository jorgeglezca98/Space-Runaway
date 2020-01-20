public class PlayerDestructionController : DestructionController
{

    protected override void Initialize()
    {
        // GameEventsController.eventController.OnHealthPctChanged += PlayImpactSound;
    }

    protected override void InflictBulletDamage(string bulletType)
    {
        if (bulletType == "enemy_shot_prefab(Clone)")
        {
            Stats.SetHealth(Stats.GetHealth() - 1f);
            PlayImpactSound(0f);
            GameEventsController.eventController.HealthPctChanged(Stats.GetHealth());
        }
    }

    protected override void PlayImpactSound(float damage)
    {
        audioManager.PlaySoundEffect("OnPlayerImpact");
    }

    protected override void DestroySpaceship()
    {
        //SplitSpaceship();
        PlayExplosionSound();
        //Destroy(gameObject, destructionDelay);
        GameEventsController.eventController.PlayerDestroyed();
    }

}
