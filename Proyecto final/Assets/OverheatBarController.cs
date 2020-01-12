using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBarController : MonoBehaviour {

	// Use this for initialization
	private Slider OverheatBar;

	// Use this for initialization
	void Start () {
		OverheatBar = gameObject.GetComponent<Slider>();
		GameEventsController.eventController.OnOverheatPctChanged += ChangeOverheat;
	}

	void ChangeOverheat(float overheat){
		this.OverheatBar.value = overheat;
	}

}
