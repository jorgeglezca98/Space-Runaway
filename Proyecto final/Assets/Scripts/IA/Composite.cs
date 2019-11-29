class Composite : Behavior {

	protected Vector<Behavior> Children = new List<Behavior>();

	public void AddChild(Behavior b) {
		Children.Add(b);
	}

    public void RemoveChild(Behavior b) {
    	Children.Remove(b);
    }

    public void ClearChildren() {
    	Children.Clear();
    }
}