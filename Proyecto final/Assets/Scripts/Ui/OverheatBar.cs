using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OverheatBar : MonoBehaviour {

	public Image foregroundImage;
	private float updateSpeedSeconds = 0.5f;

	void Start(){
		GameEventsController.eventController.OnOverheatPctChanged += HandleOverheatChanged;
	}

	// This function is called when the overheat is modified
	// we start a co-routine to soft the effect of the overheat change.
	void HandleOverheatChanged(float pct){
		Debug.Log("Im here!\n");
		StartCoroutine(ChangeToPct(pct));
	}

	// We're substracting to the foreground image a little of the
	// overheat percentage that has changed each time.
	private IEnumerator ChangeToPct(float pct){
		Debug.Log(pct);
		float preChangePct = foregroundImage.fillAmount;
		float elapsed = 0f;
		while(elapsed < updateSpeedSeconds){
			elapsed += Time.deltaTime;
			if(pct == 0)	// Only when the overheat is zero we want to soft the overheat bar fill changing.
				foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
			else foregroundImage.fillAmount = pct;
			yield return null;
		}
		foregroundImage.fillAmount = pct;
	}
	// Update is called once per frame
	void Update () {

	}
}
