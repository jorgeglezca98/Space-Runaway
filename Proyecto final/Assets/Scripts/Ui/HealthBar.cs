using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HealthBar : MonoBehaviour {

	public Image foregroundImage;
	private float updateSpeedSeconds = 0.5f;

	void Awake(){
		GameEventsController.eventController.OnHealthPctChanged += HandleHealthChanged;
	}

	// This function is called when the health is modified
	// we start a co-routine to soft the effect of the health change.
	void HandleHealthChanged(float pct){
		StartCoroutine(ChangeToPct(pct));
	}

	// We're substracting to the foreground image a little of the
	// health percentage that has changed each time.
	private IEnumerator ChangeToPct(float pct){
		float preChangePct = foregroundImage.fillAmount;
		float elapsed = 0f;
		while(elapsed < updateSpeedSeconds){
			elapsed += Time.deltaTime;
			foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
			yield return null;
		}
		foregroundImage.fillAmount = pct;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
