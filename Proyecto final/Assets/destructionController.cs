using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructionController : MonoBehaviour {

	public float life = 100f;
	public int destructionDelay = 10;

	void OnCollisionEnter(Collision collision) {

		life -= collision.relativeVelocity.magnitude + collision.rigidbody.mass;

		if(life <= 0) {
	    	Vector3 center = GetComponent<Renderer>().bounds.center;
	    	float radius = GetComponent<Renderer>().bounds.size.z;

	    	foreach (Transform child in transform) {
	            child.transform.parent = null;
	            Rigidbody childrg = child.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
	            childrg.useGravity = false;
	            childrg.AddExplosionForce(1000, center, radius);
	            Destroy(child.gameObject, destructionDelay);
	        }

	        Destroy(gameObject, destructionDelay);
	    }
    }
}
