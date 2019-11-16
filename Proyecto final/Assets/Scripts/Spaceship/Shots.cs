using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class shots : MonoBehaviour {

	public GameObject shotPrefab;
	public int shotSpeed = 2000;
	public float shotMaxDistance = 100f;
	private Transform shotPoint;
	private GameObject lastShot;
	private float lastShotSize;

	// Time since the last time the player attacked.
	private float attackModeTimer = 0.0f;
	// Maximum time the player is in attack mode
	// after he stops attacking (in seconds).
	private float maxAttackModeTimer = 2f;

	// The amount of overheat the weapon produces everytime it shots.
	private float overheatIncrement = 0.25f;
	// The amount of overheat the weapon cools down everytime it shots.
	private float overheatDecrement = 0.0625f;
	// The amount of time in seconds the weapon get disabled when the
	// maximum overheat is achieved.
	private float maxOverheatPenalization = 5f;

	// Use this for initialization
	void Start () {

		GameEventsController.eventController.OnMaximumOverheat += coolDown;

		shotPoint = null;
		foreach(Transform child in transform)
		{
		    if(child.tag == "shotPoint"){
		    	shotPoint = child;
		    	break;
		    }
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
		if(PlayerStats.getOverheat() < PlayerStats.getMaxOverheat()){
			if(Input.GetButton("Shoot") && (lastShot == null || Vector3.Distance(shotPoint.position, lastShot.transform.position - new Vector3(0,0,lastShotSize/2)) > lastShotSize)) {
				RaycastHit hit;
				GameObject HUD = GameObject.FindWithTag("playerHUD");

				if (Physics.Raycast(HUD.GetComponent<Renderer>().bounds.center, HUD.transform.TransformDirection(Vector3.forward), out hit, shotMaxDistance, ~(1 << 8)))
	            {
	                transform.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
	            } else {
	            	transform.localRotation = Quaternion.Euler(0,0,0);
	            }

				GameObject shot = Instantiate(shotPrefab, shotPoint.position, transform.rotation);

				Rigidbody shotRb = shot.AddComponent<Rigidbody>();
				shotRb.useGravity = false;
	            shotRb.AddRelativeForce(new Vector3(0,0,shotSpeed));

	            lastShot = shot;
	            lastShotSize = lastShot.GetComponent<Renderer>().bounds.size.z;

			if(!PlayerStats.isAttacking()){
				PlayerStats.setAttackMode(true);
			}

			attackModeTimer = Time.time + maxAttackModeTimer;
			PlayerStats.setOverheat(PlayerStats.getOverheat() + overheatIncrement);
			}
		}
	}

	void Update(){
		manageAttackMode();
		if(PlayerStats.getOverheat() < PlayerStats.getMaxOverheat()){
			PlayerStats.setOverheat(PlayerStats.getOverheat() - overheatDecrement);
		}
	}

	void coolDown(){
		StartCoroutine("coolDown_");
	}

	IEnumerator coolDown_(){
		yield return new WaitForSeconds(maxOverheatPenalization);
		PlayerStats.setOverheat(0f);
	}

	// When the time in "maxAttackModeTimer" has passed
	// the attack mode sets to false.
	void manageAttackMode(){
		if(PlayerStats.isAttacking()){
			if(attackModeTimer <= Time.time){
				PlayerStats.setAttackMode(false);
			}
		}
	}



}
