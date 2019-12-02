namespace BehaviorTree {

	class BehaviorTree {

		protected Behavior root;

		public BehaviorTree(Behavior initialNode) {
			root = initialNode;
		}

		public void Tick() {
			root.Tick();
		}
	}	
}