using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestructionController : DestructionController {

	protected override void Initialize(){
		// GameEventsController.eventController.OnHealthPctChanged += PlayImpactSound;
	}

	protected override void InflictBulletDamage(string bulletType){
		if(bulletType == "enemy_shot_prefab(Clone)"){
			Stats.setHealth(Stats.getHealth() - 1f);
			PlayImpactSound(0f);
			GameEventsController.eventController.healthPctChanged(Stats.getHealth());
		}
	}

	protected override void PlayImpactSound(float damage){
			AudioManager.PlaySoundEffect("OnPlayerImpact");
	}

	protected override void DestroySpaceship(){
		//SplitSpaceship();
		PlayExplosionSound();
		//Destroy(gameObject, destructionDelay);
		GameEventsController.eventController.playerDestroyed();
	}

}
