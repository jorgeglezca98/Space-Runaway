using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace BehaviorTree {

	class IsNearTarjet : LeafNode {

		public IsNearTarjet(GameObject agent) : base(agent) {}

	    public override Status Update() {
	    	double distance = (Agent.transform.position - ArtificialIntelligence.target.transform.position).magnitude;
	    	Vector3 agentSize = Agent.GetComponent<Renderer>().bounds.size;
	    	Vector3 targetSize = ArtificialIntelligence.target.GetComponent<Renderer>().bounds.size;
	    	//(Math.Max(agentSize.x, Math.Max(agentSize.y, agentSize.z)) + Math.Max(targetSize.x, Math.Max(targetSize.y, targetSize.z))) * 2);
			return (distance < 50) ? Status.BH_SUCCESS : Status.BH_FAILURE;
	    }
	}
}