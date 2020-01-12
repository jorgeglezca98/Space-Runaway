using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionController : DestructionController {

	protected override void Initialize(){

	}

	protected override void InflictBulletDamage(){
		Stats.setHealth(Stats.getHealth() - 5f);
		PlayImpactSound(0f);
	}

	protected override void DestroySpaceship(){
		SplitSpaceship();
		PlayExplosionSound();
		Destroy(gameObject, destructionDelay);
	}


}
