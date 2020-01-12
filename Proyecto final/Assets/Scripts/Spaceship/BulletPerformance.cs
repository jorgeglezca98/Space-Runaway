using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPerformance : MonoBehaviour {

	private int maxDistace = 100;
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

	void OnTriggerEnter(Collider other){
			if(other.gameObject.tag != "bullet") {
				destroyBullet();
			}
    }

	void destroyBullet(){
		var exp = GetComponent<ParticleSystem>();
        exp.Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
		Destroy(gameObject, exp.duration);
	}
}
