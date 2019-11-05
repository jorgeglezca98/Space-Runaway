using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
    {
    	Vector3 center = GetComponent<Renderer>().bounds.center;
    	float radius = GetComponent<Renderer>().bounds.size.z;

    	foreach (Transform child in transform)
        {
            child.transform.parent = null;
            Rigidbody childrg = child.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            childrg.useGravity = false;
            childrg.AddExplosionForce(1000, center, radius);
        }
    }
}
