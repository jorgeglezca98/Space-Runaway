using UnityEngine;

namespace BehaviorTree {

	class MoveBack : LeafNode {

		private int Velocity;

		public MoveBack(GameObject agent, int velocity) : base(agent) {
			Velocity = velocity;
		}

	    public override Status Update() {
	    	Agent.transform.Translate(0, 0, - Time.deltaTime * Velocity);
	    	return Status.BH_SUCCESS;
	    }
	}
}