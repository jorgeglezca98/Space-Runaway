using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class ArtificialIntelligence : MonoBehaviour {

	public static GameObject Target;
	private BehaviorTree.BehaviorTree Tree;
	public int Velocity = 1500;
	public GameObject ShotPrefab;
	public int ShotMaxDistance = 200;
	public int ShotMinDistance = 10;
	public int ShotSpeed = 2000;
	public int DistanceFarFromTarget = 100;
	public int DistanceCloseToTarget = 50;
	public int AimingHelpRange = 100;

	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		if(players.Length > 0 && Target == null)
			Target = players[0];

		Selector root = new Selector();
		Sequence sequenceRetroceder = new Sequence();
		sequenceRetroceder.AddChild(new IsTheTarjetClose(gameObject, DistanceCloseToTarget));
		sequenceRetroceder.AddChild(new MoveBack(gameObject, Velocity));
		Sequence sequenceAvanzar = new Sequence();
		sequenceAvanzar.AddChild(new IsTheTarjetFar(gameObject, DistanceFarFromTarget));
		sequenceAvanzar.AddChild(new MoveAlong(gameObject, Velocity));
		root.AddChild(sequenceRetroceder);
		root.AddChild(sequenceAvanzar); 
		root.AddChild(new Shoot(gameObject, ShotPrefab, ShotMaxDistance, ShotSpeed, AimingHelpRange, ShotMinDistance));
		Tree = new BehaviorTree.BehaviorTree(root);
	}

	void Update() {
		Tree.Tick();
	}
}