using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private Vector3 PlayerInitialPosition = new Vector3(0, 0, 0);
    private GameObject PlayerSpaceship;

	void Awake () {
        PlayerSpaceship = GameObject.Find("PlayerSpaceship");
        PlayerSpaceship.transform.position = PlayerInitialPosition;
        AddScriptsToPlayer();
	}

	// Update is called once per frame
	void Update () {

	}

    void AddScriptsToPlayer()
    {
        PlayerSpaceship.AddComponent<PlayerDestructionController>();
        PlayerSpaceship.AddComponent<SpaceshipMovement>();
        PlayerSpaceship.AddComponent<SpotlightController>();
        PlayerSpaceship.transform.Find("Blaster-1").gameObject.AddComponent<Shots>();
        PlayerSpaceship.transform.Find("Blaster-2").gameObject.AddComponent<Shots>();
        GameObject.Find("FrontSpotlight").AddComponent<SpotlightController>();
    }





}
