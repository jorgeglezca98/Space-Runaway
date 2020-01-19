using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour
{

    protected int destructionDelay = 1;
    // private int Health = 200;
    public LifeStats Stats;
    protected AudioManager AudioManager;
    bool SpaceshipHasBeenDestroyed = false;

    private void Start()
    {
        // Stats = new LifeStats(Health);
        SetHealth();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }


    private void SetHealth()
    {
        if (gameObject.name == "PlayerSpaceship")
        {
            Stats = new LifeStats(200);
        }
        else if (gameObject.name == "AssaultEnemy(Clone)")
        {
            Stats = new LifeStats(200);
        }
        else if (gameObject.name == "KamikazeEnemy(Clone)")
        {
            Stats = new LifeStats(500);
        }
    }

    void Update()
    {
        if (Stats.getHealth() <= 0 && !SpaceshipHasBeenDestroyed)
        {
            DestroySpaceship();
            SpaceshipHasBeenDestroyed = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision enter!");
        if (collision.gameObject.tag == "bullet")
            InflictBulletDamage(collision.gameObject.name);
        else if (collision.gameObject.tag != "mothership") {
            Debug.Log("Muerte por " + collision.gameObject.tag);
            DestroySpaceship();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter!");
        if (other.gameObject.tag == "bullet")
            InflictBulletDamage(other.gameObject.name);
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void InflictBulletDamage(string bulletType)
    {

    }

    protected virtual void DestroySpaceship()
    {

    }

    protected virtual void PlayImpactSound(float damage)
    {

    }

    public void PlayExplosionSound()
    {
        AudioManager.PlaySoundEffect("Explosion");
    }

    public void SplitSpaceship()
    {
        Vector3 center = GetComponent<Renderer>().bounds.center;
        float radius = GetComponent<Renderer>().bounds.size.z;

        foreach (Transform child in transform)
        {
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
