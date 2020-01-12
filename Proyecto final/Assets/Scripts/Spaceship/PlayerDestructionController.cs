using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestructionController : DestructionController {

	protected override void Initialize(){
		GameEventsController.eventController.OnHealthPctChanged += PlayImpactSound;
	}

	protected override void InflictBulletDamage(){
		Stats.setHealth(Stats.getHealth() - 5f);
		GameEventsController.eventController.healthPctChanged(Stats.getHealth());
	}

	protected override void DestroySpaceship(){
		SplitSpaceship();
		PlayExplosionSound();
		Destroy(gameObject, destructionDelay);
	}

}
