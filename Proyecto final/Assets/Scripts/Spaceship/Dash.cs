
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dash : MonoBehaviour
{
    Vector3 rightDash = new Vector3(Time.deltaTime * 350, 0, 0);
    Vector3 leftDash = new Vector3(-Time.deltaTime * 350, 0, 0);
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddRelativeForce(leftDash, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rb.AddRelativeForce(rightDash, ForceMode.Impulse);
        }
    }
}
