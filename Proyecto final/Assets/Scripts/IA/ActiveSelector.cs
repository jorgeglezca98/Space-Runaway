class ActiveSelector : Composite {

	protected IEnumerator<Behabior> CurrentChild;

    protected virtual void OnInitialize() {
      CurrentChild = Children.GetEnumerator();
    }

	Status ActiveSelector::update() {
	    Behaviors::iterator prev = m_Current;
	    Selector::onInitialize();
	    Status result = Selector::update();
	    if (prev != m_Children.end() && m_Current != prev)
	        (*previous)->abort();
	    return result;
	}
}