using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BehaviorTree;

public class ArtificialIntelligence : MonoBehaviour {

	public static GameObject target;
	private BehaviorTree.BehaviorTree tree;
	public static int velocity = 1500;

	void Start () {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		if(players.Length > 0 && target == null)
			target = players[0];

		Selector root = new Selector();
		Sequence sequenceRetroceder = new Sequence();
		sequenceRetroceder.AddChild(new IsNearTarjet(gameObject));
		sequenceRetroceder.AddChild(new Retrocede(gameObject));
		Sequence sequenceAvanzar = new Sequence();
		sequenceAvanzar.AddChild(new IsFarTarjet(gameObject));
		sequenceAvanzar.AddChild(new Avance(gameObject));
		root.AddChild(sequenceRetroceder);
		root.AddChild(sequenceAvanzar);
		tree = new BehaviorTree.BehaviorTree(root);
	}

	void Update() {
		tree.Tick();
	}
}