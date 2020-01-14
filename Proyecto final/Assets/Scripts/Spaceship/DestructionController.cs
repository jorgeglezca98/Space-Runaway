using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour {

    protected int destructionDelay = 1;
    private int Health = 200;
    public LifeStats Stats;
    private AudioManager AudioManager;
		bool SpaceshipHasBeenDestroyed = false;

    private void Start()
    {
        Stats = new LifeStats(Health);
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

		void Update(){
			if (Stats.getHealth() <= 0 && !SpaceshipHasBeenDestroyed){
				DestroySpaceship();
				SpaceshipHasBeenDestroyed = true;
			}
		}

    void OnCollisionEnter(Collision collision)
	  {
			if(collision.gameObject.tag == "bullet")
				InflictBulletDamage();
			else if (collision.gameObject.tag != "mothership")
        DestroySpaceship();
    }

    private void OnTriggerEnter(Collider other)
    {
			if(other.gameObject.tag == "bullet")
				InflictBulletDamage();
			else if(other.gameObject.tag != "mothership")
        DestroySpaceship();
    }

		protected virtual void Initialize(){

		}

		protected virtual void InflictBulletDamage(){

		}

		protected virtual void DestroySpaceship(){

		}

		/*It receives a parameter because the Delegate requires it*/
		public void PlayImpactSound(float damage){
			AudioManager.PlaySoundEffect("Impact");
		}

		public void PlayExplosionSound(){
			AudioManager.PlaySoundEffect("Explosion");
		}

		public void SplitSpaceship(){
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
		}




}
