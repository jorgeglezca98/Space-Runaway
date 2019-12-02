using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
  
  class Selector : Composite {
      
      protected IEnumerator<Behavior> CurrentChild;

      public override void OnInitialize() {
        CurrentChild = Children.GetEnumerator();
      }

      public override Status Update() {
        //Keep going until a child behavior says it’s running.
        if (!CurrentChild.MoveNext()) return Status.BH_FAILURE;
        while (true) {
          Status s = CurrentChild.Current.Tick();
          //If child succeeds or keeps running, do the same.
          if (s != Status.BH_FAILURE) return s;
          //Continue search for fallback until the last child.
          bool end = !CurrentChild.MoveNext();
          if (end)
            return Status.BH_FAILURE;
        }
        return Status.BH_INVALID;//”Unexpected loop exit.”
      }
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
