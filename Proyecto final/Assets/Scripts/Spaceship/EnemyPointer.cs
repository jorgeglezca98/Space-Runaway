using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour {

	private GameObject enemy;

	private float lastHealth;
	private DateTime lastShot;

	private Renderer ArrowRenderer;
	private DestructionController PlayerDestructionController;

	void Start () {
		ArrowRenderer = gameObject.GetComponent<Renderer> ();
		ArrowRenderer.material.color = Color.blue;

		PlayerDestructionController = GameObject.FindWithTag("Player").GetComponent<DestructionController>();
		lastHealth = PlayerDestructionController.Stats.getHealth();
		
		lastShot = DateTime.Now;
	}

	void Update () {
		if(!enemy) {
			enemy = GameObject.FindWithTag("enemy");
		}
		if(enemy) {
			transform.LookAt(enemy.transform);
			transform.Rotate(90, 0, 0);

			if(lastHealth != PlayerDestructionController.Stats.getHealth()) {
				lastHealth = PlayerDestructionController.Stats.getHealth();
				lastShot = DateTime.Now;
				ArrowRenderer.material.color = Color.red;
			} else if((DateTime.Now - lastShot).TotalSeconds >= 1f) {
				ArrowRenderer.material.color = Color.blue;
			}
		}
	}
}
