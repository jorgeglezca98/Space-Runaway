using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody rb;
    private float intensity = 600;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (Input.GetButtonDown("DashIzq"))
        {
            rb.AddRelativeForce(new Vector3(-Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
            audioManager.PlaySoundEffect("Dash");
        }
        else if (Input.GetButtonDown("DashDch"))
        {
            rb.AddRelativeForce(new Vector3(Time.deltaTime * intensity, 0, 0), ForceMode.Impulse);
            audioManager.PlaySoundEffect("Dash");
        }
    }
}
