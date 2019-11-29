class Monitor : Parallel {

    public void AddCondition(Behavior condition) {
       Children.Insert(0, condition);
    }

    public void AddAction(Behavior action) {
       Children.Add(action);
    }
}