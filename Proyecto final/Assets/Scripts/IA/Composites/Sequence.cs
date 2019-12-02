using UnityEngine;

namespace BehaviorTree {

  class Sequence : Composite {

    public override Status Update() {
      //Keep going until a child behavior says it’s running.
      try {
        foreach (Behavior child in Children) {
          Status s = child.Tick();
          //If child fails or keeps running, do the same.
          if (s != Status.BH_SUCCESS) return s;

        }
        return Status.BH_SUCCESS;//Unexpected loop exit.
      } catch {
        return Status.BH_INVALID;
      }
    }
  }
}