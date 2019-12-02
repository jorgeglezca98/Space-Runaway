namespace BehaviorTree {
	
	abstract class Decorator : Behavior {

		protected Behavior Child;

		public Decorator(Behavior child) {
			Child = child;
		}
	}
}