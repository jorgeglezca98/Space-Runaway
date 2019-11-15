using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	// This delegate ensures that every function related with
	// health changing is called.
	public event Action<float> OnHealthPctChanged = delegate {};
	private float maxHealth = 100f;

	private void OnEnable(){
		PlayerStats.setHealth(maxHealth);
	}

	// We calculate the amount of health the player restores or
	// loses and call the functions associated with health changing.
	public void modifyHealth(float amount){
		float currentHealth = PlayerStats.getHealth() + amount;
		PlayerStats.setHealth(currentHealth);
		float healthPct = (float)currentHealth / (float)maxHealth;
		OnHealthPctChanged(healthPct);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			modifyHealth(-10);
		}
	}
}
