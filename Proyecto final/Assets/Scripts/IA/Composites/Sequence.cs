using UnityEngine;

namespace BehaviorTree
{
    // TODO: improve syntax, make the for loop simpler
    class Sequence : Composite
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
                        if (s != Status.BH_SUCCESS) return s;

                    }
                    return Status.BH_SUCCESS;
                }
                else
                {
                    int i = 0;
                    while (i < Children.Count && Children[i].GetStatus() != Status.BH_RUNNING)
                        i++;

                    for(; i < Children.Count; i++)
                    {
                        Status s = Children[i].Tick();
                        if (s != Status.BH_SUCCESS) return s;
                    }
                    return Status.BH_SUCCESS;
                }
            }
            catch
            {
                return Status.BH_INVALID;
            }
        }
    }
}
