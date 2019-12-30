using UnityEngine;

namespace BehaviorTree {

	class MoveAlong : LeafNode {

		private int Velocity;

		public MoveAlong(GameObject agent, int velocity) : base(agent) {
			Velocity = velocity;
		}
	    public override Status Update() {
	    	//Agent.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, Time.deltaTime * Velocity));
            Agent.transform.Translate(0, 0, Time.deltaTime * Velocity);
            return Status.BH_SUCCESS;
	    }
	}
}