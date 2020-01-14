using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipIndicator : MonoBehaviour {


	private GameObject MotherShip;
	private Camera Camera;
	private float Width;
	private float Height;
	private float TopCornerX;
	private float TopCornerY;
	// Use this for initialization
	void Start () {
		MotherShip = GameObject.Find("MotherShip");
		Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		Width = GetComponent<RectTransform>().sizeDelta.x;
		Height = GetComponent<RectTransform>().sizeDelta.y;
		// Debug.Log("Height: " + Height);
		TopCornerX = (-1) * (Width/2);
		TopCornerY = (Height/2);
		// Debug.Log("Top Corner Y : " + TopCornerY);
		// Camera = GameObject.Find("CameraEmptyObject").gameObject.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 MotherShipPosRelativeToCam = Camera.WorldToViewportPoint(MotherShip.transform.position);

		float moterShipFacing = MotherShipPosRelativeToCam.z;
		float x = TopCornerX + (Width * MotherShipPosRelativeToCam.x);
		float y = - TopCornerY + (Height * MotherShipPosRelativeToCam.y);
		float z = 0;
		Vector3 FixedMotherShipPosRelativeToCam = new Vector3(x,y,z);
		Debug.Log("Fixed pos: " + FixedMotherShipPosRelativeToCam);
		// Debug.Log("Z: " + z);
		if(moterShipFacing  > 0)
			transform.Find("MotherShipIcon").GetComponent<RectTransform>().localPosition = FixedMotherShipPosRelativeToCam;
	}
}
