using Behavior;

class Decorator : Behavior {

	protected Behavior Child;

	public Decorator(Behavior child) {
		Child = child;
	}
}