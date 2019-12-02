using System;
using UnityEngine;

namespace BehaviorTree {

	abstract class Behavior {

		private Status status = Status.BH_INVALID;

		public virtual void OnInitialize() {}
		public abstract Status Update();
		public virtual void OnTerminate(Status s) {}

	    public Status Tick() {
	       if (status != Status.BH_RUNNING) OnInitialize();
	       status = Update();
	       if (status != Status.BH_RUNNING) OnTerminate(status);
	       return status;
		}

		public override bool Equals(System.Object obj) {
			if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
				return false;
			} else {
				return this == obj;
			}   
	    }

	    public override int GetHashCode() {
	        return base.GetHashCode();
	    }
	}
}