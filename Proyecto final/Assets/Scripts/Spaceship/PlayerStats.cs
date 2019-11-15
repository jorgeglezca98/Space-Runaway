using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{

	private static float playerHealth = 100f;
	private static float playerExhaust = 0f;


	public static float getHealth(){
		return playerHealth;
	}

	public static void setHealth(float health){
		playerHealth = health;
	}

	public static float getExhaust(){
		return playerExhaust;
	}

	public static void setExhaust(float exhaust){
		playerExhaust = exhaust;
	}
}
