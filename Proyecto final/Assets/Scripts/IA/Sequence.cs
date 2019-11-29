class Sequence : Composite {

    public virtual Status Update() {
      //Keep going until a child behavior says it’s running.
      try {
        foreach (Behavior child in Children) {
          Status s = child.Tick();
          //If child fails or keeps running, do the same.
          if (s != BH_SUCCESS) return s;

        }
        return BH_SUCCESS;//Unexpected loop exit.
      } catch {
        return BH_INVALID;
      }
    }
}