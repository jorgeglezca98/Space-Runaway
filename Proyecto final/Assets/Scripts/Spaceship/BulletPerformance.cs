using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPerformance : MonoBehaviour
{
    private int maxDistace = 200;
    private Vector3 initialPosition;
    private bool imTriggered = false;

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) >= maxDistace)
        {
            destroyBullet();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "bullet" && !imTriggered)
        {
            imTriggered = true;
            destroyBullet();
        }
    }

    void destroyBullet()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject, exp.duration);
    }
}
