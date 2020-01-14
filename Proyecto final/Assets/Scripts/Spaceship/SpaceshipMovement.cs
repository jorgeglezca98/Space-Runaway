using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour {

	private Rigidbody rg;
    private AudioManager AudioManager;

	private int velocidadRotacion = 500;
	private int velocidad = 1800;

    private float NoticeableSoundThreshold = 1f;
    private bool MovementSoundEffectPlaying;

	// Use this for initialization
	void Start () {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		rg = GetComponent<Rigidbody>();
		rg.drag = 0.5f;
		rg.angularDrag = 0.5f;
		rg.centerOfMass = Vector3.zero;
	}

	// Update is called once per frame
	void FixedUpdate () {
        MovementSoundEffectPlaying = AudioManager.FindSoundEffect("Movement").IsPlaying();
        //Debug.Log("Magnitude: " + rg.velocity.magnitude);

        if (rg.velocity.magnitude < NoticeableSoundThreshold && MovementSoundEffectPlaying)
        {
            Debug.Log("Audio being stopped!");
            AudioManager.StopSoundEffect("Movement");
        }

		rg.AddRelativeTorque((Vector3.back * Input.GetAxis("Horizontal") + Vector3.right * Input.GetAxis("Vertical")) * Time.deltaTime * velocidadRotacion);
		rg.AddRelativeForce(new Vector3(0, 0, (Input.GetButton("Run") ? 1 : 0) - (Input.GetButton("Stop") ? 1 : 0)) * Time.deltaTime * velocidad);

        if (Input.GetButton("Run") || Input.GetButton("Stop")){
            if (!MovementSoundEffectPlaying)
                AudioManager.PlaySoundEffect("Movement");
        }else{
            if(MovementSoundEffectPlaying)
                AudioManager.StopSoundEffect("Movement");
        }





    }
}
