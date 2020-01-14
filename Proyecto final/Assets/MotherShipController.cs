using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipController : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "Player"){
			GameEventsController.eventController.playerWon();
		}
	}

}
