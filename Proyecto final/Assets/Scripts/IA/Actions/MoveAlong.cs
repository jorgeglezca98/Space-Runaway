using UnityEngine;

namespace BehaviorTree {

	class MoveAlong : LeafNode {

		private int Velocity;
		private Rigidbody rb;

		public MoveAlong(GameObject agent, int velocity) : base(agent) {
			Velocity = velocity;
			// rb = agent.GetComponent<Rigidbody>();
		}

	    public override Status Update() {
						Debug.Log("Agent forward: " + Agent.transform.forward);
						Debug.Log("Time: " + Time.deltaTime);
						Debug.Log("Velocity: " + Velocity);
						Debug.Log("The AI name is: " + Agent.transform.name);
						// GameObject.Find("AssaultEnemy(Clone)").transform.Translate(Time.deltaTime * Velocity * Vector3.forward);
            Agent.transform.Translate(Time.deltaTime * Velocity * Agent.transform.forward);
						// Agent.transform.position += Agent.transform.TransformDirection(new Vector3(0, 0, Time.deltaTime * Velocity));
						// Agent.transform.position += Agent.transform.forward * Time.deltaTime * Velocity;
						// rb.velocity += Agent.transform.forward * Time.deltaTime * Velocity;
						// rb.MovePosition(Agent.transform.position + Agent.transform.forward * Velocity * Time.deltaTime);
					  return Status.BH_SUCCESS;
	    }
	}
}
