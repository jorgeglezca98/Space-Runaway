using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour {

	public int destructionDelay = 1;
    public LifeStats Stats = new LifeStats();

	void OnCollisionEnter(Collision collision) 
	{
		DestroySpaceship(collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroySpaceship(other.gameObject.tag);
    }

    private void DestroySpaceship(string tag){
    	if(tag == "bullet") 
			Stats.setHealth(Stats.getHealth() - 0.5f);
		else
			Stats.setHealth(0f);

		if(Stats.getHealth() <= 0) 
		{
	    	Vector3 center = GetComponent<Renderer>().bounds.center;
	    	float radius = GetComponent<Renderer>().bounds.size.z;

	    	foreach (Transform child in transform) {
	            child.transform.parent = null;
	            Rigidbody childrg = child.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
	            childrg.useGravity = false;
	            childrg.AddExplosionForce(1000, center, radius);
	            Destroy(child.gameObject, destructionDelay);
	        }

	        var ps = GetComponent<ParticleSystem>();
	        ps.Play();
	        Destroy(gameObject, destructionDelay);
	    }
    }
}
