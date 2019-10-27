using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPerformance : MonoBehaviour {

	public int maxDistace = 100;
	private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(initialPosition, transform.position) >= maxDistace){
			destroyBullet();
		}
	}

	void OnCollisionEnter(Collision collision)
    {
		destroyBullet();
    }

	void destroyBullet(){
		Destroy(gameObject);
	}
}
