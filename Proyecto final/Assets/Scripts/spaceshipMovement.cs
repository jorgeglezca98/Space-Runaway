using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipMovement : MonoBehaviour {

	private Rigidbody rg;
	
	public int velocidadRotacion = 200;
	public int velocidad = 200;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody>();
		rg.drag = 0.5f;
		rg.angularDrag = 0.5f;
		rg.centerOfMass = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rg.AddRelativeTorque((Vector3.back * Input.GetAxis("Horizontal") + Vector3.left * Input.GetAxis("Vertical")) * Time.deltaTime * velocidadRotacion);
		rg.AddRelativeForce(new Vector3(0, 0, (Input.GetKey(KeyCode.K) ? 1 : 0) - (Input.GetKey(KeyCode.L) ? 1 : 0)) * Time.deltaTime * velocidad);
	}
}
