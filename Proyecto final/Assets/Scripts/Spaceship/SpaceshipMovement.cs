using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour {

	private Rigidbody rg;

	public int velocidadRotacion = 500;
	public int velocidad = 1800;

	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody>();
		rg.drag = 0.5f;
		rg.angularDrag = 0.5f;
		rg.centerOfMass = Vector3.zero;
	}

	// Update is called once per frame
	void FixedUpdate () {
		rg.AddRelativeTorque((Vector3.back * Input.GetAxis("Horizontal") + Vector3.right * Input.GetAxis("Vertical")) * Time.deltaTime * velocidadRotacion);
		rg.AddRelativeForce(new Vector3(0, 0, (Input.GetButton("Run") ? 1 : 0) - (Input.GetButton("Stop") ? 1 : 0)) * Time.deltaTime * velocidad);
	}
}
