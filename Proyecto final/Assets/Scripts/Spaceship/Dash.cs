
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dash : MonoBehaviour
{

    public Rigidbody rb;
    public float intensity = 600;
    private AudioManager AudioManager;

    private void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (Input.GetButtonDown("DashIzq"))
        {
            rb.AddRelativeForce(new Vector3(-Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
            AudioManager.PlaySoundEffect("Dash");
        }
        else if (Input.GetButtonDown("DashDch"))
        {
            rb.AddRelativeForce(new Vector3(Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
            AudioManager.PlaySoundEffect("Dash");
        }
    }
}
