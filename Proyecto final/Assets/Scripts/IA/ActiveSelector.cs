using System.Collections.Generic;

namespace BehaviorTree {
	
	class ActiveSelector : Composite {
	/*
		protected IEnumerator<Behavior> CurrentChild;

	    protected virtual void OnInitialize() {
	      CurrentChild = Children.GetEnumerator();
	    }

		public override Status Update() {
		    IEnumerator<Behavior> prev = CurrentChild;
		    Selector::onInitialize();
		    Status result = Selector::update();
		    if (prev != m_Children.end() && m_Current != prev)
		        (*previous)->abort();
		    return result;
		} */
		
		public override Status Update(){
			return Status.BH_FAILURE;
		}
	}
}