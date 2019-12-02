using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

namespace BehaviorTree {

	class IsTheTarjetClose : LeafNode {

		private int ClosenessLimit;

		public IsTheTarjetClose(GameObject agent, int closenessLimit) : base(agent) {
			ClosenessLimit = closenessLimit;
		}

	    public override Status Update() {
	    	double distance = (Agent.transform.position - ArtificialIntelligence.Target.transform.position).magnitude;
			return (distance < ClosenessLimit) ? Status.BH_SUCCESS : Status.BH_FAILURE;
	    }
	}
}