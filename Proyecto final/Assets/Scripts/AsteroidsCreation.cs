using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsCreation : MonoBehaviour {

	// This ratio works great!! (Ratio = 5)
	private int areaSize = 1000;
	private int asteroidGap = 200;
	private GameObject currentMeteor;


	private List<string> asteroidNames = new List<string>{ "Asteroid_S_00", "Asteroid_M_00", "Asteroid_L_00",
																									"Asteroid_XL_00", "Asteroid_XXL_00", "Asteroid_S_20",
																									"Asteroid_M_20", "Asteroid_L_20", "Asteroid_XL_20",
																									"Asteroid_XXL_20", "Asteroid_M_80", "Asteroid_L_80"};
	void Awake()
	{
		int minRange = -1 * (areaSize / asteroidGap);
		int maxRange = areaSize / asteroidGap;

		for(int i = minRange ; i < maxRange; i++){
			for(int j = minRange ; j < maxRange; j++){
				for(int k = minRange ; k < maxRange; k++){
					int asteroidName = Random.Range(0,12);
					int yAxis = i * asteroidGap;
					int xAxis = j * asteroidGap;
					int zAxis = k * asteroidGap;
					currentMeteor = Instantiate(Resources.Load(asteroidNames[asteroidName], typeof(GameObject)), new Vector3(xAxis,yAxis,zAxis), Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360))) as GameObject;
					currentMeteor.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10,11),Random.Range(-10,11),Random.Range(-10,11)), ForceMode.Impulse);
				}
			}
		}

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
