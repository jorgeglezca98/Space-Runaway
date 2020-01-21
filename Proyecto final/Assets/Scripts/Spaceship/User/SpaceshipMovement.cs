using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    private Rigidbody rg;
    private AudioManager audioManager;

    private int velocidadRotacion = 500;
    private int velocidad = 1800;

    private float noticeableSoundThreshold = 1f;
    private bool movementSoundEffectPlaying;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rg = GetComponent<Rigidbody>();
        rg.drag = 0.5f;
        rg.angularDrag = 0.5f;
        rg.centerOfMass = Vector3.zero;
    }

    private void FixedUpdate()
    {
        movementSoundEffectPlaying = audioManager.FindSoundEffect("Movement").IsPlaying();
        //Debug.Log("Magnitude: " + rg.velocity.magnitude);

        if (rg.velocity.magnitude < noticeableSoundThreshold && movementSoundEffectPlaying)
        {
            //Debug.Log("Audio being stopped!");
            audioManager.StopSoundEffect("Movement");
        }

        float horizontalInput = Input.GetAxis("HorizontalGamepad");
        float verticalInput = Input.GetAxis("VerticalGamepad");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            horizontalInput = Input.GetAxis("HorizontalJoystick");
            verticalInput = Input.GetAxis("VerticalJoystick");
        }

        rg.AddRelativeTorque((Vector3.back * horizontalInput + Vector3.right * verticalInput) * Time.deltaTime * velocidadRotacion);
        rg.AddRelativeForce(new Vector3(0, 0, (Input.GetButton("Run") ? 1 : 0) - (Input.GetButton("Stop") ? 1 : 0)) * Time.deltaTime * velocidad);

        if (Input.GetButton("Run") || Input.GetButton("Stop"))
        {
            if (!movementSoundEffectPlaying)
            {
                audioManager.PlaySoundEffect("Movement");
            }
        }
        else
        {
            if (movementSoundEffectPlaying)
            {
                audioManager.StopSoundEffect("Movement");
            }
        }
    }
}

