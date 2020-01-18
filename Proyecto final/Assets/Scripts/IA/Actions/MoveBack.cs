using UnityEngine;

namespace BehaviorTree {

	class MoveBack : LeafNode {

		private int Velocity;
		private Rigidbody rb;

		public MoveBack(GameObject agent, int velocity) : base(agent) {
			Velocity = velocity;
			rb = agent.GetComponent<Rigidbody>();
		}

	    public override Status Update() {
				Debug.Log("Moving back!");
	    	// Agent.transform.Translate(Time.deltaTime * Velocity * -Vector3.forward);
				// GameObject.Find("AssaultEnemy(Clone)").transform.Translate(Time.deltaTime * Velocity * -Vector3.forward);
				// Agent.transform.Translate(0,0,Time.deltaTime * Velocity * -Agent.transform.forward.z);
				Agent.transform.position -= new Vector3(0,0,Time.deltaTime * Velocity * Agent.transform.forward.z);
				// Agent.transform.position -= Agent.transform.TransformDirection(new Vector3(0, 0, Time.deltaTime * Velocity));
				// Agent.transform.position -= Agent.transform.forward * Time.deltaTime * Velocity;
				// rb.velocity -= Agent.transform.forward  * Time.deltaTime * Velocity;
				// rb.MovePosition(Agent.transform.position - Agent.transform.forward * Velocity * Time.deltaTime);
				return Status.BH_SUCCESS;
	    }
	}
}
