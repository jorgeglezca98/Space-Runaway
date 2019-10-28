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

	// Use this for initialization
	void Start () {
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
		if(Input.GetKey(KeyCode.Space) && (lastShot == null || Vector3.Distance(shotPoint.position, lastShot.transform.position - new Vector3(0,0,lastShotSize/2)) > lastShotSize)) {
			RaycastHit hit;
			GameObject HUD = GameObject.FindWithTag("playerHUD");

			if (Physics.Raycast(HUD.GetComponent<Renderer>().bounds.center, HUD.transform.TransformDirection(Vector3.forward), out hit, shotMaxDistance))
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
		}
	}
}
