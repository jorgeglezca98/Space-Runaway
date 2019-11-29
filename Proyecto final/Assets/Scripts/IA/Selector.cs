class Selector : Composite {
    
    protected IEnumerator<Behabior> CurrentChild;

    protected virtual void OnInitialize() {
      CurrentChild = Children.GetEnumerator();
    }

    protected virtual Status Update() {
      //Keep going until a child behavior says it’s running.
      while (true) {
        Status s = CurrentChild.Current.tick();
        //If child succeeds or keeps running, do the same.
        if (s != BH_FAILURE) return s;
        //Continue search for fallback until the last child.
        bool end = !CurrentChild.MoveNext();
        if (end)
          return BH_FAILURE;
      }
      return BH_INVALID;//”Unexpected loop exit.”
    }
}

//class Selector : Composite {
//
//    public virtual Status Update() {
//      try {
//        foreach (Behavior child in children ) {
//          Status s = child.Tick();
//          if (s != BH_FAILURE) return s;
//
//       }
//        return BH_FAILURE;
//      } catch {
//        return BH_INVALID;
//      }
//    }
//}
