using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
	//TODO: Maybe maxOverheat, maxHealth and overheatWaitTime should go into another class
	// and this should be only for stats!

	private static float attackModeTime = 5f;
	private static bool  attackMode = false;
	private static float playerHealth = 100f;
	private static float maxHealth = 100f;
	private static float playerOverheat = 0f;
	private static float maxOverheat = 100f;
	private static float overheatWaitTime = 5f;


	public static float getHealth(){
		return playerHealth;
	}

	public static void setHealth(float health){
		playerHealth = health;
		float healthPct = playerHealth / maxHealth;
		GameEventsController.eventController.healthPctChanged(healthPct);
	}

	public static float getOverheat(){
		return playerOverheat;
	}

	public static float getMaxOverheat(){
		return maxOverheat;
	}

	public static void setOverheat(float overheat){
		if(overheat > 0f)
			playerOverheat = overheat;
		else playerOverheat = 0f;

		float overheatPct = playerOverheat / maxOverheat;
		GameEventsController.eventController.overheatPctChanged(overheatPct);

		if(playerOverheat >= maxOverheat){
			GameEventsController.eventController.maximumOverheat();
		}
	}

	public static bool isAttacking(){
		return attackMode;
	}

	public static void setAttackMode(bool attacking){
		attackMode = attacking;
		if(attacking){
			Debug.Log("Attacking!");
			GameEventsController.eventController.attackModeEnter();
		}else{
			Debug.Log("Releasing!");
			GameEventsController.eventController.attackModeExit();
		}
	}

	public static float getAttackModeTime(){
		return attackModeTime;
	}

	public static void setAttackModeTime(float time){
		attackModeTime = time;
	}


}
