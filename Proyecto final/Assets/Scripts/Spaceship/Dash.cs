
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dash : MonoBehaviour
{

    public Rigidbody rb;
    public float intensity = 600;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.AddRelativeForce(new Vector3(-Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddRelativeForce(new Vector3(Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
        }
    }
}
