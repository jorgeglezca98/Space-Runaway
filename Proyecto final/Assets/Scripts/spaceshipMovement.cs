using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipMovement : MonoBehaviour {

	private Rigidbody rg;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rg.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime, ForceMode.Impulse);
		
		if(rg.rotation.z < -0.25){
			rg.constraints = RigidbodyConstraints.FreezeRotationZ;
		} else {
			rg.AddTorque(Vector3.back * Input.GetAxis("Horizontal") * Time.deltaTime * 200);
		}
	}
}
