using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStats{

	private float playerHealth = 100f;
	private float maxHealth = 100f;

	public float getHealth(){
		return playerHealth;
	}

	public void setHealth(float health){
		playerHealth = health;
		float healthPct = playerHealth / maxHealth;	
	}

}