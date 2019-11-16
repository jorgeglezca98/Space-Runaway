using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsController : MonoBehaviour {

	// You need an instance to call the events!
	public static GameEventsController eventController;

	// Health delegate.
	public event Action<float> OnHealthPctChanged = delegate{};
	public event Action<float> OnOverheatPctChanged = delegate{};
	public event Action OnMaximumOverheat = delegate{};
	public event Action OnAttackModeEnter = delegate{};
	public event Action OnAttackModeExit = delegate{};


	public void healthPctChanged(float health){
		if(OnHealthPctChanged != null){
			OnHealthPctChanged(health);
		}
	}

	public void overheatPctChanged(float overheat){
		if(OnOverheatPctChanged != null){
			OnOverheatPctChanged(overheat);
		}
	}

	public void maximumOverheat(){
		if(OnMaximumOverheat != null){
			OnMaximumOverheat();
		}
	}

	public void attackModeEnter(){
		if(OnAttackModeEnter != null){
			OnAttackModeEnter();
		}
	}

	public void attackModeExit(){
		if(OnAttackModeExit != null){
			OnAttackModeExit();
		}
	}

	// Use this for initialization
	void Awake () {
		eventController = this;
	}

	// Update is called once per frame
	void Update () {

	}
}
