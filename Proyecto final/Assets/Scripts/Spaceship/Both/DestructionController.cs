using UnityEngine;

public class DestructionController : MonoBehaviour
{
    public LifeStats Stats { get; set; }

    protected int destructionDelay = 1;
    protected AudioManager audioManager;

    private bool spaceshipHasBeenDestroyed = false;
    
    private void Awake()
    {
        SetHealth();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

    private void Update()
    {
        if (Stats.GetHealth() <= 0 && !spaceshipHasBeenDestroyed)
        {
            DestroySpaceship();
            spaceshipHasBeenDestroyed = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision enter!");
        if (collision.gameObject.tag != "mothership")
        {
            DestroySpaceship();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger enter!");
        if (other.gameObject.tag == "bullet")
        {
            InflictBulletDamage(other.gameObject.name);
        }
        else if (other.gameObject.tag != "mothership")
        {
            DestroySpaceship();
        }
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
        audioManager.PlaySoundEffect("Explosion");
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
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
    }
}
