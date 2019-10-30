using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsCreation : MonoBehaviour {

	private GameObject A_S_00;
	private GameObject A_M_00;
	private GameObject A_L_00;
	private GameObject A_XL_00;
	private GameObject A_XXL_00;
	private GameObject A_S_20;
	private GameObject A_M_20;
	private GameObject A_L_20;
	private GameObject A_XL_20;
	private GameObject A_XXL_20;
	private GameObject A_M_80;
	private GameObject A_L_80;

	void Awake()
	{
		A_S_00 = Instantiate(Resources.Load("Asteroid_L_20", typeof(GameObject))) as GameObject;
		Instantiate(A_S_00, new Vector3(14,0,14), Quaternion.identity);
		Debug.Log(A_S_00.GetComponent<Collider>().bounds.size);
	}
	// Use this for initialization
	void Start () {

	}

	void createAsteroids(){

	}

	// Update is called once per frame
	void Update () {

	}
}
