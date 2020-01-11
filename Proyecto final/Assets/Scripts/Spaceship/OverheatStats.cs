using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatStats{

	private float attackModeTime = 5f;
	private bool  attackMode = false;
	private float playerOverheat = 0f;
	private float maxOverheat = 100f;
	private float overheatWaitTime = 5f;

	public float getOverheat(){
		return playerOverheat;
	}

	public float getMaxOverheat(){
		return maxOverheat;
	}

	public void setOverheat(float overheat){
		if(overheat > 0f)
			playerOverheat = overheat;
		else 
			playerOverheat = 0f;
	}

	public bool isAttacking(){
		return attackMode;
	}

	public void setAttackMode(bool attacking){
		attackMode = attacking;
	}

	public float getAttackModeTime(){
		return attackModeTime;
	}

	public void setAttackModeTime(float time){
		attackModeTime = time;
	}


}