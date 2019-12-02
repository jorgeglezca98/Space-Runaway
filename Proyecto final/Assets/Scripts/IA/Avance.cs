using UnityEngine;

namespace BehaviorTree {

	class Avance : LeafNode {

		public Avance(GameObject agent) : base(agent) {}

	    public override Status Update() {
	    	Agent.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, Time.deltaTime * ArtificialIntelligence.velocity));
	    	return Status.BH_SUCCESS;
	    }
	}
}