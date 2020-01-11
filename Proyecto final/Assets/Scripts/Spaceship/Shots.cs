using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shots : MonoBehaviour {

	public GameObject shotPrefab;
	public int shotSpeed = 2000;
	public float ShotMaxDistance = 100f;
	public float ShotMinDistance = 15f; // If it is less than 10 it could be problematic
	public float RangeSize = 10f;
	private Transform shotPoint;
	private GameObject lastShot;
	private float lastShotSize;
	private OverheatStats Stats = new OverheatStats();

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
		if(Stats.getOverheat() < Stats.getMaxOverheat()){
			if(Input.GetButton("Shoot") && (lastShot == null || Vector3.Distance(shotPoint.position, lastShot.transform.position - new Vector3(0,0,lastShotSize/2)) > lastShotSize)) {
				RaycastHit hit;
				GameObject HUD = GameObject.FindWithTag("playerHUD");

				RaycastHit[] hitsInRange = Physics.BoxCastAll(HUD.transform.position, 
					new Vector3(RangeSize, RangeSize, ShotMinDistance), 
					transform.TransformDirection(Vector3.forward), 
					Quaternion.identity, 
					ShotMaxDistance, 
					(1 << 10));

				if(hitsInRange.Length > 0) {
					int i = 0;
					Vector3 origenRaycast = HUD.transform.position;
					do {
						Physics.Raycast(
							origenRaycast, 
							hitsInRange[i].transform.position - origenRaycast, 
							out hit, 
							Mathf.Infinity, 
							~(1 << 8) & ~(1 << 9));
						i++;
					} while (hit.transform.tag != "enemy" && i < hitsInRange.Length);
					
					Debug.Log(hit.transform.tag);
					if (hit.transform.tag == "enemy")
			        {
			        	transform.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
		            } else {
		            	transform.localRotation = Quaternion.Euler(0,0,0);
		            }
				} else {
					transform.localRotation = Quaternion.Euler(0,0,0);
				}

				GameObject shot = Instantiate(shotPrefab, shotPoint.position, transform.rotation);

				Rigidbody shotRb = shot.GetComponent<Rigidbody>();
	            shotRb.AddRelativeForce(new Vector3(0,0,shotSpeed));

	            lastShot = shot;
	            lastShotSize = lastShot.GetComponent<Renderer>().bounds.size.z;

				if(!Stats.isAttacking()){
					Stats.setAttackMode(true);
				}

				attackModeTimer = Time.time + maxAttackModeTimer;
				Stats.setOverheat(Stats.getOverheat() + overheatIncrement);
			}
		}
	}

	void Update(){
		manageAttackMode();
		if(Stats.getOverheat() < Stats.getMaxOverheat()){
			Stats.setOverheat(Stats.getOverheat() - overheatDecrement);
		}
	}

	void coolDown(){
		StartCoroutine("coolDown_");
	}

	IEnumerator coolDown_(){
		yield return new WaitForSeconds(maxOverheatPenalization);
		Stats.setOverheat(0f);
	}

	// When the time in "maxAttackModeTimer" has passed
	// the attack mode sets to false.
	void manageAttackMode(){
		if(Stats.isAttacking()){
			if(attackModeTimer <= Time.time){
				Stats.setAttackMode(false);
			}
		}
	}

}
