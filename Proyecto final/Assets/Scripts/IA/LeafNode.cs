using UnityEngine;

namespace BehaviorTree {

	abstract class LeafNode : Behavior {

		protected GameObject Agent;

		public LeafNode(GameObject agent) {
			Agent = agent;
		}
	}
}