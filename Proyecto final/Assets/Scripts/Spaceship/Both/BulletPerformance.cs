using UnityEngine;

public class BulletPerformance : MonoBehaviour
{
    private int maxDistace = 200;
    private Vector3 initialPosition;
    private bool imTriggered = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) >= maxDistace)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "bullet" && !imTriggered)
        {
            imTriggered = true;
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject, exp.main.duration);
    }
}
