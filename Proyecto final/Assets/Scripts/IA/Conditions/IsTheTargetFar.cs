using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace BehaviorTree {

	class IsTheTarjetFar : LeafNode {

		private int FarLimit;

		public IsTheTarjetFar(GameObject agent, int farLimit) : base(agent) {
			FarLimit = farLimit;
		}

	    public override Status Update() {
	    	double distance = (Agent.transform.position - ArtificialIntelligence.Target.transform.position).magnitude;
			return (distance > FarLimit) ? Status.BH_SUCCESS : Status.BH_FAILURE;
	    }
	}
}