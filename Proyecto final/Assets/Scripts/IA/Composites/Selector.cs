using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    class Selector : Composite
    {
        public override Status Update()
        {
            //Keep going until a child behavior says it’s running.
            try
            {
                if (GetStatus() != Status.BH_RUNNING) // Sequence Status
                {
                    foreach (Behavior child in Children)
                    {
                        Status s = child.Tick();
                        if (s != Status.BH_FAILURE) return s;

                    }
                    return Status.BH_FAILURE;
                }
                else
                {
                    int i = 0;
                    while (i < Children.Count && Children[i].GetStatus() != Status.BH_RUNNING)
                        i++;

                    for (; i < Children.Count; i++)
                    {
                        Status s = Children[i].Tick();
                        if (s != Status.BH_FAILURE) return s;
                    }
                    return Status.BH_FAILURE;
                }
            }
            catch
            {
                return Status.BH_INVALID;
            }
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
