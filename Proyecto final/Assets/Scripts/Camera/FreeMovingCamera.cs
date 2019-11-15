using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovingCamera : MonoBehaviour {

	public int movementSpeed;
	public float mouseSensitivity = 100.0f;
	public float clampAngle = 80.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.W)){
			transform.position += Vector3.forward * movementSpeed;
		}

		if(Input.GetKey(KeyCode.S)){
			transform.position += Vector3.back* movementSpeed;
		}

		if(Input.GetKey(KeyCode.A)){
			transform.position += Vector3.left * movementSpeed;
		}

		if(Input.GetKey(KeyCode.D)){
			transform.position += Vector3.right * movementSpeed;
		}

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
		transform.rotation = localRotation;

	}
}
